angular.module("sbAdmin", ["authenticate", "ngRoute", "crud","ckeditor"])
       .config(function ($routeProvider) {
           $routeProvider.when("/dashboard", {
               templateUrl: "/SBAdmin/app/views/page-wrapper/dashboard.html"
           });
           //quan ly nhan vien
           $routeProvider.when("/quan-ly/nhan-vien", {
               templateUrl: "/SBAdmin/app/views/manage/employee/index.html"
           });
           $routeProvider.when("/quan-ly/nhan-vien/them", {
               templateUrl: "/SBAdmin/app/views/manage/employee/create.html"
           });
           $routeProvider.when("/quan-ly/nhan-vien/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/manage/employee/update.html"
           });

           //quan ly nha mang
           $routeProvider.when("/quan-ly/nha-mang", {
               templateUrl: "/SBAdmin/app/views/manage/network/index.html"
           });
           $routeProvider.when("/quan-ly/nha-mang/them", {
               templateUrl: "/SBAdmin/app/views/manage/network/create.html"
           });
           $routeProvider.when("/quan-ly/nha-mang/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/manage/network/update.html"
           });
           // quan ly dai ly
           $routeProvider.when("/quan-ly/dai-ly", {
               templateUrl: "/SBAdmin/app/views/manage/supplier/index.html"
           });
           $routeProvider.when("/quan-ly/dai-ly/them", {
               templateUrl: "/SBAdmin/app/views/manage/supplier/create.html"
           });
           $routeProvider.when("/quan-ly/dai-ly/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/manage/supplier/update.html"
           });
           // quan ly loai sim
           $routeProvider.when("/quan-ly/loai-sim", {
               templateUrl: "/SBAdmin/app/views/manage/simtype/index.html"
           });
           $routeProvider.when("/quan-ly/loai-sim/them", {
               templateUrl: "/SBAdmin/app/views/manage/simtype/create.html"
           });
           $routeProvider.when("/quan-ly/loai-sim/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/manage/simtype/update.html"
           });
           // quan ly danh muc
           $routeProvider.when("/quan-ly/menu", {
               templateUrl: "/SBAdmin/app/views/manage/menu/index.html"
           });
           $routeProvider.when("/quan-ly/menu/them", {
               templateUrl: "/SBAdmin/app/views/manage/menu/create.html"
           });
           $routeProvider.when("/quan-ly/menu/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/manage/menu/update.html"
           });
           // quan ly tin tuc
           $routeProvider.when("/quan-ly/tin-tuc", {
               templateUrl: "/SBAdmin/app/views/manage/new/index.html"
           });
           $routeProvider.when("/quan-ly/tin-tuc/them", {
               templateUrl: "/SBAdmin/app/views/manage/new/create.html"
           });
           $routeProvider.when("/quan-ly/tin-tuc/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/manage/new/update.html"
           });
           // thao tao voi sim
           $routeProvider.when("/sim", {
               templateUrl: "/SBAdmin/app/views/working/sim/index.html"
           });
           $routeProvider.when("/sim/them", {
               templateUrl: "/SBAdmin/app/views/working/sim/create.html"
           });
           $routeProvider.when("/sim/gui-bang-so", {
               templateUrl: "/SBAdmin/app/views/working/sim/import.html"
           });
           $routeProvider.when("/sim/kiem-tra-sim", {
               templateUrl: "/SBAdmin/app/views/working/sim/check.html"
           });
           $routeProvider.when("/sim/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/working/sim/update.html"
           });
           // thao tac voi order
           $routeProvider.when("/giao-dich", {
               templateUrl: "/SBAdmin/app/views/working/order/index.html"
           });
           $routeProvider.when("/giao-dich/them", {
               templateUrl: "/SBAdmin/app/views/working/order/create.html"
           });
           $routeProvider.when("/giao-dich/cap-nhat/:id", {
               templateUrl: "/SBAdmin/app/views/working/order/update.html"
           });
           //error
           $routeProvider.when("/error", {
               templateUrl: "/SBAdmin/app/views/page-wrapper/error.html"
           });
           //ban lam viec
           $routeProvider.otherwise({
               templateUrl: "/SBAdmin/app/views/page-wrapper/dashboard.html"
           });
       });