angular.module("sbAdmin")
.controller("orderCtrl", function ($location, $scope, $routeParams, crudService, Authentication) {
    $scope.lstOrder = [];
    // init
    crudService.getAll("/Order/GetAll")
        .success(function (data) {
            angular.forEach(data, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
                item.LastUpdate = parseDate(item.LastUpdate);
            });
            $scope.lstOrder = data;
        })
        .error(function (error) {
            console.log(error);
        });
    //
    $scope.create = function (data) {
        $("#myModal").modal("show");
        crudService.get("/SIM/GetByNumber?number=", data.Number)
            .success(function (sim) {
                data.SIM_ID = sim.ID;
                data.Price = sim.Price;
                var currentUser = Authentication.currentUser();
                data.CreateBy = currentUser.Name;
                data.UserBussiness = currentUser.Name;
                crudService.create("/Order/Create", data)
                    .success(function (data) {
                        console.log(data);
                        $("#myModal").modal("hide");
                        $location.path("/giao-dich/")
                    })
                    .error(function (error) {
                        $("#myModal").modal("hide");
                        console.log(error);
                    });
            })
            .error(function (error) {
                $("#myModal").modal("hide");
                alert("SIM ko tồn tại!")
                console.log(error);
            })
    }
    // update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/Order/Get/", id)
            .success(function (data) {
                data.CreateDate = parseDate(data.CreateDate);
                data.LastUpdate = parseDate(data.LastUpdate);
                $scope.orderUpdate = data;
            })
            .error(function (error) {
                console.log(error);
            });
    }
    $scope.update = function (data) {
        $("#myModal").modal("show");
        crudService.get("/SIM/GetByNumber?number=", data.Number)
            .success(function (sim) {
                data.SIM_ID = sim.ID;
                data.Price = sim.Price;
                var currentUser = Authentication.currentUser();
                data.UpdateBy = currentUser.Name;
                crudService.update("/Order/Update", data)
                    .success(function (data) {
                        $("#myModal").modal("hide");
                        $location.path("/giao-dich/")
                    })
                    .error(function (error) {
                        $("#myModal").modal("hide");
                        console.log(error);
                    });
            })
            .error(function (error) {
                $("#myModal").modal("hide");
                alert("SIM ko tồn tại.")
                console.log(error);
            })
    }
    //delete
    $scope.remove = function (id) {
        crudService.remove("/Order/Delete", id)
            .success(function (data) {
                $location.path("/giao-dich/");
            })
            .error(function (error) {
                console.log(error);
            });
    }
    // function
    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
});