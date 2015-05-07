angular.module("sbAdmin")
.controller("appCtrl", function ($scope, Authentication) {
    $scope.currentUser = function () {
        return Authentication.currentUser();
    }
})