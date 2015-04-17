angular.module("sbAdmin")
.controller("simtypeCtrl", function ($scope, crudService, $location, $routeParams) {
    $scope.lstSimType = [];
    $scope.lstSTName = [];
    // init
    crudService.getAll("/SimType/GetListSimType")
        .success(function (data) {
            $scope.lstSimType = data;
        })
        .error(function (error) {
            console.log(error);
        })
    crudService.get("/SimType/SimTypeName", "")
        .success(function (data) {
            $scope.lstSTName = data;
        })
        .error(function (error) {
            console.log(error);
        })
    //create
    $scope.create = function (data) {
        $("#myModal").modal("show");
        data.isActive = true;
        data.isDeleted = false;
        crudService.create("/SimType/Create", data)
            .success(function (data) {
                $("#myModal").modal("hide");
                $location.path("/thiet-lap-danh-muc/loai-sim/")
            })
            .error(function (error) {
                console.log(error);
            })
    }
    // update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/SimType/Get/", id)
            .success(function (data) {
                $scope.stUpdate = data;
            })
            .error(function (error) {
                console.log(error);
            });
    }
    $scope.update = function (data) {
        $("#myModal").modal("show");
        crudService.update("/SimType/Update", data)
            .success(function (data) {
                $("#myModal").modal("hide");
                $location.path("/thiet-lap-danh-muc/loai-sim/")
            })
            .error(function (error) {
                console.log(error);
            })
    }
    //remove
    $scope.remove = function (data) {
        data.isActive = false;
        data.isDeleted = true;
        crudService.update("/SimType/Update", data)
            .success(function (data) {
                $location.path("/thiet-lap-danh-muc/loai-sim/")
            })
            .error(function (error) {
                console.log(error);
            })
    }
});