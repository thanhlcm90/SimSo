angular.module("sbAdmin")
.controller("authenCtrl", function ($scope, $rootScope, Authentication, $location, $window) {

    $scope.signIn = function (user) {
        $("#myModal").modal("show");
        Authentication.signIn("/Account/SignIn", user)
            .success(function (result) {
                if (result == "Login Success") {
                    $("#myModal").modal("hide");
                    Authentication.getCurrentUser("/Account/GetCurrentUser")
                        .success(function (data) {
                            Authentication.setCurrentUser(data);
                            Authentication.signedIn = "True";
                            $location.path("/");
                        });
                } else {
                    $("#myModal").modal("hide");
                    alert(result);
                }
            })
            .error(function (error) {
                $("#myModal").modal("hide");
                alert(error);
            });
    }

    $scope.signOut = function () {
        Authentication.signOut("/Account/SignOut")
            .success(function () {
                Authentication.signedIn = "False";
                Authentication.setCurrentUser();
                $location.path("/signIn");
                alert("Signed out!");
            })
    }
    $scope.currentUser = function () {
        return Authentication.currentUser();
    }
})