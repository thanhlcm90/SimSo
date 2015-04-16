angular.module("sbAdmin")
.controller("supplierCtrl", function ($scope, crudService) {
    $scope.lstSupplier = [];

    crudService.getAll("/Supplier/GetListSupplier")
        .success(function (data) {
            $scope.lstSupplier = data;
        })
        .error(function (error) {
            console.log(error);
        })
    $scope.create = function (data) {
        crudService.create("/Supplier/Create", data)
            .success(function (data) {
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })
    }
    $scope.update = function (data) {
        crudService.update("/Supplier/Update", data)
            .success(function (data) {
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })
    }
    $scope.remove = function (id) {
        crudService.remove("/Supplier/Delete", id)
            .success(function (data) {
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })
    }
});