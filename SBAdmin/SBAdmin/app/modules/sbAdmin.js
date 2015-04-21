angular.module("sbAdmin", ["authenticate", "ngRoute", "crud"])
       .config(function ($routeProvider) {
           $routeProvider.when("/dashboard", {
               templateUrl: "/SBAdmin/app/views/page-wrapper/dashboard.html"
           });
           //sign in
           $routeProvider.when("/signIn", {
               templateUrl: "/SBAdmin/app/views/authentication/signIn.html"
           });
           //manage user
           $routeProvider.when("/quan-ly-nhan-vien", {
               templateUrl: "/SBAdmin/app/views/manageUser/index.html"
           });
           $routeProvider.when("/quan-ly-nhan-vien/them", {
               templateUrl: "/SBAdmin/app/views/manageUser/create.html"
           });
           $routeProvider.when("/quan-ly-nhan-vien/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/manageUser/update.html"
           });

           //menu-network
           $routeProvider.when("/thiet-lap-danh-muc/nha-mang", {
               templateUrl: "/SBAdmin/app/views/menu/network/index.html"
           });
           $routeProvider.when("/thiet-lap-danh-muc/nha-mang/them", {
               templateUrl: "/SBAdmin/app/views/menu/network/create.html"
           });
           $routeProvider.when("/thiet-lap-danh-muc/nha-mang/sua/:id", {
               templateUrl: "/SBAdmin/app/views/menu/network/update.html"
           });
           // supplier
           $routeProvider.when("/dai-ly", {
               templateUrl: "/SBAdmin/app/views/menu/supplier/index.html"
           });
           $routeProvider.when("/dai-ly/them", {
               templateUrl: "/SBAdmin/app/views/menu/supplier/create.html"
           });
           $routeProvider.when("/dai-ly/sua/:id", {
               templateUrl: "/SBAdmin/app/views/menu/supplier/update.html"
           });
           // menu-sim type
           $routeProvider.when("/thiet-lap-danh-muc/loai-sim", {
               templateUrl: "/SBAdmin/app/views/menu/simtype/index.html"
           });
           $routeProvider.when("/thiet-lap-danh-muc/loai-sim/them", {
               templateUrl: "/SBAdmin/app/views/menu/simtype/create.html"
           });
           $routeProvider.when("/thiet-lap-danh-muc/loai-sim/sua/:id", {
               templateUrl: "/SBAdmin/app/views/menu/simtype/update.html"
           });
           // menu-sim
           $routeProvider.when("/quan-ly-sim", {
               templateUrl: "/SBAdmin/app/views/menu/sim/index.html"
           });
           $routeProvider.when("/quan-ly-sim/them", {
               templateUrl: "/SBAdmin/app/views/menu/sim/create.html"
           });
           $routeProvider.when("/quan-ly-sim/sua/:id", {
               templateUrl: "/SBAdmin/app/views/menu/sim/update.html"
           });
           // menu order
           $routeProvider.when("/quan-ly-giao-dich", {
               templateUrl: "/SBAdmin/app/views/menu/order/index.html"
           });
           $routeProvider.when("/quan-ly-giao-dich/them", {
               templateUrl: "/SBAdmin/app/views/menu/order/create.html"
           });
           $routeProvider.when("/quan-ly-giao-dich/sua/:id", {
               templateUrl: "/SBAdmin/app/views/menu/order/update.html"
           });
           //ban lam viec
           $routeProvider.otherwise({
               templateUrl: "/SBAdmin/app/views/page-wrapper/dashboard.html"
           });
       });