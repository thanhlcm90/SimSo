angular.module("sbAdmin")
.controller("supplierCtrl", function ($location, $scope, $routeParams, crudService, Authentication) {
    $scope.lstSupplier = [];
    // init
    crudService.getAll("/Supplier/GetListSupplier")
        .success(function (data) {
            //  console.log(data);
            angular.forEach(data, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
                item.LastUpdate = parseDate(item.LastUpdate);
            });
            $scope.lstSupplier = data;
        })
        .error(function (error) {
            console.log(error);
        });
    //
    $scope.create = function (data) {
        $("#myModal").modal("show");
        var currentUser = Authentication.currentUser();
        data.CreateBy = currentUser.Name;
        data.isActive = true;
        data.isDeleted = false;
        crudService.create("/Supplier/Create", data)
            .success(function (data) {
                console.log(data);
                $("#myModal").modal("hide");
                $location.path("/dai-ly/")
            })
            .error(function (error) {
                $("#myModal").modal("hide");
                console.log(error);
            })
    }
    // update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/Supplier/Get/", id)
            .success(function (data) {
                console.log(data);
                data.CreateDate = parseDate(data.CreateDate);
                data.LastUpdate = parseDate(data.LastUpdate);
                $scope.spUpdate = data;
            })
            .error(function (error) {
                console.log(error);
            });
    }
    $scope.update = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.isActive = true;
        data.isDeleted = false;
        crudService.update("/Supplier/Update", data)
            .success(function (data) {
                $location.path("/dai-ly/");
            })
            .error(function (error) {
                console.log(error);
            })
    }
    //delete
    $scope.remove = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.isActive = false;
        data.isDeleted = true;
        crudService.update("/Supplier/Update", data)
            .success(function (data) {
                $location.path("/dai-ly/");
            })
            .error(function (error) {
                console.log(error);
            });
    }
    // function
    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
});