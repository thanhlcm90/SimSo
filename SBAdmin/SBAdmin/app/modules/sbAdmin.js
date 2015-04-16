﻿angular.module("sbAdmin", ["authenticate", "ngRoute", "crud"])
       .config(function ($routeProvider) {
           $routeProvider.when("/dashboard", {
               templateUrl: "/SBAdmin/app/views/page-wrapper/dashboard.html"
           });
           //sign in
           $routeProvider.when("/signIn", {
               templateUrl: "/SBAdmin/app/views/authentication/signIn.html"
           });
           //manage user
           $routeProvider.when("/manageUser", {
               templateUrl: "/SBAdmin/app/views/page-wrapper/manageUser.html"
           });
           $routeProvider.when("/manageUser/create", {
               templateUrl: "/SBAdmin/app/views/manageUser/create.html"
           });
         
           //menu-network
           $routeProvider.when("/thiet-lap-danh-muc/nha-mang", {
               templateUrl: "/SBAdmin/app/views/menu/network/index.html"
           });
           $routeProvider.when("/thiet-lap-danh-muc/nha-mang/them", {
               templateUrl: "/SBAdmin/app/views/menu/network/create.html"
           });
           $routeProvider.when("/thiet-lap-danh-muc/nha-mang/sua", {
               templateUrl: "/SBAdmin/app/views/menu/network/update.html"
           });

           $routeProvider.otherwise({
               templateUrl: "/SBAdmin/app/views/page-wrapper/dashboard.html"
           });
       });

angular.module("sbAdmin").run(['Authentication', '$location', function (Authentication, $location) {
    if (Authentication.signedIn == "False") {
        $location.path('/signIn');
    } else {
        Authentication.getCurrentUser("/Account/GetCurrentUser")
            .success(function (data) {
                Authentication.setCurrentUser(data);
            });
    }
}]);