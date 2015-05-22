angular.module("sbAdmin")
.controller("appCtrl", function ($scope, Authentication) {
    $scope.isInRole = function (role) {
        if (Authentication.currentUser()) {
            if (Authentication.currentUser().Role == role) {
                return true;
            }
            return false;
        }
    }
})
.controller("dashboardCtrl", function ($scope, crudService) {
    $scope.lstNewOrder = [];
    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
    crudService.getAll("/Order/GetTopOrders")
        .success(function (data) {
            angular.forEach(data, function (item) {
                item.LastUpdate=parseDate(item.LastUpdate)
            })
            $scope.lstNewOrder = data;
        })
        .error(function (err) {
            console.log(err);
        })
})