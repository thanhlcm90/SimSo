angular.module("sbAdmin")
.controller("getSIMCtrl", function ($scope, crudService, $location, $routeParams) {
    $scope.lstSIM = [];
    $scope.lstSimType = [];
    $scope.lstNetwork = [];
    $scope.lstSupplier = [];
    // init
    crudService.getAll("/SIM/GetAll")
        .success(function (data) {
            $scope.lstSIM = data;
        })
        .error(function (error) {
            console.log(error);
        })
})
.controller("crudSIMCtrl", function ($scope, crudService, $http, $routeParams, $location, Authentication) {
    // models
    $scope.lstSimType = [];
    $scope.lstNetwork = [];
    $scope.lstSupplier = [];
    $scope.SIM = {};
    //init
    crudService.getAll("/NetWork/GetListNetWork")
        .success(function (data) {
            $scope.lstNetwork = data;
        })
        .error(function (error) {
            console.log(error);
        })

    crudService.getAll("/SimType/GetListSimType")
        .success(function (data) {
            $scope.lstSimType = data;
        })
        .error(function (error) {
            console.log(error);
        })
    crudService.getAll("/Supplier/GetListSupplier")
        .success(function (data) {
            $scope.lstSupplier = data;
        })
        .error(function (error) {
            console.log(error);
        })
    // tự động nhận loại nhà mạng
    $scope.changeNumber = function () {
        if ($scope.SIM.Number && ($scope.SIM.Number.length >= 3)) {
            var number = $scope.SIM.Number;
            var count = $scope.lstNetwork.length;
            for (i = 0; i < count; i++) {
                item = $scope.lstNetwork[i];
                if (item.Number.indexOf(number.substring(0, 3)) > -1 || item.Number.indexOf(number.substring(0, 4)) > -1) {
                    $scope.SIM.NetWork_ID = item.ID;
                    break;
                } else {
                    $scope.SIM.NetWork_ID = 0;
                }
            }
        } else {
            $scope.SIM.NetWork_ID = 0;
        }
    }
    //create
    $scope.create = function (data) {
        $("#myModal").modal("show");
        data.CreateBy = Authentication.currentUser().Name;
        data.isActive = true;
        data.isDeleted = false;
        crudService.create("/SIM/Create", data)
            .success(function (data) {
                $("#myModal").modal("hide");
                $location.path("/quan-ly-sim/")
            })
            .error(function (error) {
                console.log(error);
            })
    }
    // update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/SIM/Get/", id)
            .success(function (data) {
                $scope.SIM = data;
            })
            .error(function (error) {
                console.log(error);
            });
    }
    $scope.update = function (data) {
        $("#myModal").modal("show");
        data.CreateDate = parseDate(data.CreateDate);
        data.LastUpdate = null;
        data.UpdateBy = Authentication.currentUser().Name;
        crudService.update("/SIM/Update", data)
            .success(function (data) {
                $("#myModal").modal("hide");
                $location.path("/quan-ly-sim/")
            })
            .error(function (error) {
                console.log(error);
            })
    }
    //remove
    $scope.remove = function (data) {
        data.isActive = false;
        data.isDeleted = true;
        crudService.update("/SIM/Update", data)
            .success(function (data) {
                $location.path("/quan-ly-sim/")
            })
            .error(function (error) {
                console.log(error);
            })
    }
    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
})