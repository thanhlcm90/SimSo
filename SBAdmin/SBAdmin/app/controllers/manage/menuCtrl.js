angular.module("sbAdmin")
.controller("menuCtrl", function ($location, $scope, $routeParams, crudService, Authentication) {
    $scope.lstMenu = [];
    // init
    crudService.getAll("/Menu/GetAll")
        .success(function (data) {
            //  console.log(data);
            angular.forEach(data, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
                item.LastUpdate = parseDate(item.LastUpdate);
            });
            $scope.lstMenu = data;
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
        crudService.create("/Menu/Create", data)
            .success(function (data) {
                $("#myModal").modal("hide");
                $location.path("/quan-ly/menu/")
            })
            .error(function (error) {
                $("#myModal").modal("hide");
                console.log(error);
            })
    }
    // update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/Menu/Get/", id)
            .success(function (data) {
                data.CreateDate = parseDate(data.CreateDate);
                data.LastUpdate = parseDate(data.LastUpdate);
                $scope.menuUpdate = data;
            })
            .error(function (error) {
                console.log(error);
            });
    }
    $scope.update = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        crudService.update("/Menu/Update", data)
            .success(function (data) {
                $location.path("/quan-ly/menu/");
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
        crudService.update("/Menu/Update", data)
            .success(function (data) {
                $location.path("/quan-ly/menu/");
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