angular.module("sbAdmin")
.controller("supplierCtrl", function ($location, $scope, $routeParams, crudService, Authentication) {
    $scope.lstSupplier = [];
    // init
    crudService.getAll("/Supplier/GetAll")
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
        var user = {};
        user.Email = data.Email;
        user.UserName = data.UserName;
        user.Password = data.Password;
        user.ConfirmPassword = data.ConfirmPassword;
        crudService.create("/Account/CreateUser", { model: user, role: "DaiLy" })
            .success(function (result) {
            })
            .error(function (error) {
                console.log(error);
            });
        var currentUser = Authentication.currentUser();
        data.CreateBy = currentUser.Name;
        data.isActive = true;
        data.isDeleted = false;
        crudService.create("/Supplier/Create", data)
            .success(function (data) {
                console.log(data);
                $("#myModal").modal("hide");
                $location.path("/quan-ly/dai-ly/")
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
        crudService.update("/Supplier/Update", data)
            .success(function (data) {
                $location.path("/quan-ly/dai-ly/");
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
                $location.path("/quan-ly/dai-ly/");
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