angular.module("sbAdmin")
.controller("manageUserCtrl", function ($scope, $location, crudService, Authentication) {
    if (Authentication.signedIn == "False") {
        $location.path('/signIn');
    } else {
        Authentication.getCurrentUser("/Account/GetCurrentUser")
           .success(function (data) {
               Authentication.setCurrentUser(data);
           });
    }
    $scope.listUser = [];
    // crud
    $scope.createUser = function (user) {
        crudService.create("/Account/CreateUser", user)
            .success(function (result) {
                $location.path('/manageUser');
            })
            .error(function (error) {
            })
    }

    $scope.deleteUser = function (userId) {
        crudService.remove("/", userId)
            .success(function (data) {
            })
            .error(function (error) {
            })
    }

    $scope.updateUser = function (user) {
        crudService.update("/", user)
            .success(function (data) {
            })
            .error(function (error) {
            })
    }

    crudService.getAll("/Account/GetUsers")
        .success(function (users) {
            $scope.listUser = users;
        })
       .error(function (error) {
       })
});