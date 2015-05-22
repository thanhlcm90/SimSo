angular.module("sbAdmin").run(['Authentication', '$http', function (Authentication, $http) {
    if (window.signedIn == "False") {
        window.location = "/cms";
    }
}]);