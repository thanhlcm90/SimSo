angular.module("sbAdmin").run(['Authentication', function (Authentication) {
    if (Authentication.signedIn == "False") {
        window.location = "/cms";
    }
}]);