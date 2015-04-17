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
.controller("crudSIMCtrl", function ($scope, crudService, $http, $routeParams, $location,Authentication) {
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
    $scope.changeNumber = function () {
        if ($scope.SIM.Number && ($scope.SIM.Number.length >= 3)) {
            $http.get("NetWork/GetByNumber/?number=" + $scope.SIM.Number)
                .success(function (data) {
                    $scope.SIM.NetWork_ID = data.ID;
                })
                .error(function (error) {
                    console.log(error);
                });
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
        data.isActive = true;
        data.isDeleted = false;
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