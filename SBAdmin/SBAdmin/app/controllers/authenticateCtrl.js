angular.module("authenticate")
.controller("authenCtrl", function ($scope, Authentication) {
    $scope.signIn = function (user) {
        $("#myModal").modal("show");
        Authentication.signIn("/Account/SignIn", user)
            .success(function (result) {
                if (result == "Login Success") {
                    $("#myModal").modal("hide");
                    window.location = "";
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
                window.location = "/Home/Login";
            })
    }
})