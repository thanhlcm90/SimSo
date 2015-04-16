angular.module("sbAdmin")
.controller("simtypeCtrl", function ($scope, crudService) {
    $scope.lstSimType = [];

    crudService.getAll("/SimType/GetListSimType")
        .success(function (data) {
            $scope.lstSimType = data;
        })
        .error(function (error) {
            console.log(error);
        })
    $scope.create = function (data) {
        crudService.create("/SimType/Create", data)
            .success(function (data) {
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })
    }
    $scope.update = function (data) {
        crudService.update("/SimType/Update", data)
            .success(function (data) {
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })
    }
    $scope.remove = function (id) {
        crudService.remove("/SimType/Delete", id)
            .success(function (data) {
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })
    }
});