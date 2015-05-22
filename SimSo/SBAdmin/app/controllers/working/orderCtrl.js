angular.module("sbAdmin")
.controller("orderCtrl", function ($location, $scope, $routeParams, crudService, Authentication, $http) {
    Authentication.authorize('QuanLy, NhanVien');
    $scope.lstItemCount = [];
    $scope.lstOrder = [];
    $scope.lstPage = [];
    $scope.currentPage = 1;
    $scope.pageSize = 20;
    $scope.pageCount = 0;
    $scope.totalItems = 0;
    $scope.currentItems = 0;
    $scope.status = null;
    $scope.orderCreate = {};
    $scope.customerOrders = [];
    var getData = function (index, pageSize, status, searchNumber) {
        return $http({
            url: "/Order/GetAll",
            method: "GET",
            params: { pageIndex: index, pageSize: pageSize, status: status, searchNumber: searchNumber }
        });
    }

    var getItemCount = function () {
        crudService.getAll("/Order/GetCountByStatus")
          .success(function (data) {
              $scope.lstItemCount = data;
          })
    }
    // init
    init = function () {
        getData(1, $scope.pageSize, $scope.status, $scope.searchNumber)
            .success(function (data) {
                angular.forEach(data.Items, function (item) {
                    item.CreateDate = parseDate(item.CreateDate);
                    item.LastUpdate = parseDate(item.LastUpdate);
                });
                $scope.lstOrder = data.Items;
                $scope.pageCount = data.PageCount;
                $scope.totalItems = data.TotalItems;
                $scope.currentItems = data.Items.length;
                for (i = 1; i <= $scope.pageCount && i <= 9; i++) {
                    $scope.lstPage.push(i);
                }
            })
            .error(function (error) {
                console.log(error);
            });

    }
    init();
    getItemCount();

    $scope.search = function () {
        init();
    }

    $scope.getCount = function (i) {
        return $scope.lstItemCount[i - 1];
    }

    $scope.filter = function (status) {
        $scope.status = status;
        $scope.lstPage.splice(0);
        init();
    }

    $scope.selectPage = function (index) {
        $("#myModal").modal("show");
        $scope.currentPage = index;
        $scope.lstPage.splice(0);
        var pageCount = $scope.pageCount;
        if (pageCount <= 9) {
            for (i = 1; i <= pageCount; i++) {
                $scope.lstPage.push(i);
            }
        } else {
            if (index >= 5) {
                if (index <= pageCount - 4) {
                    for (i = index - 4; i <= index + 4 && i <= pageCount; i++) {
                        $scope.lstPage.push(i);
                    }
                } else {
                    for (i = pageCount - 8; i <= pageCount; i++) {
                        $scope.lstPage.push(i);
                    }
                }
            } else {
                for (i = 1; i <= 9; i++) {
                    $scope.lstPage.push(i);
                }
            }
        }
        // get data
        getData(index, $scope.pageSize, $scope.status, $scope.searchNumber)
            .success(function (data) {
                angular.forEach(data.Items, function (item) {
                    item.CreateDate = parseDate(item.CreateDate);
                });
                $scope.lstOrder = data.Items;
                $scope.currentItems = data.Items.length;
                $("#myModal").modal("hide");
            })
            .error(function (error) {
                console.log(error);
            })
    }

    $scope.getClass = function (index) {
        return $scope.currentPage == index ? "active" : "";
    }

    $scope.isDisable = function (index1, index2) {
        return index1 == index2 ? "disabled" : "";
    }

    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }

    $scope.create = function (data) {
        $("#myModal").modal("show");
        crudService.get("/SIM/GetByNumber?number=", data.Number)
            .success(function (sim) {
                data.Network_ID = sim.NetWork_ID;
                data.SimType_ID = sim.SimType_ID;
                data.Sup_ID = sim.Sup_ID;
                data.Price = sim.Price;
                data.Price_Sup = sim.Price_Sup;
                var currentUser = Authentication.currentUser();
                data.CreateBy = currentUser.Name;
                data.UserBusiness = currentUser.Name;
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
    $scope.update = function (data) {
        $("#myModal").modal("show");
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.LastUpdate = null;
        crudService.update("/Order/Update", data)
            .success(function () {
                getItemCount();
                $("#myModal").modal("hide");
            })
            .error(function (error) {
                $("#myModal").modal("hide");
                console.log(error);
            })
    }

    $scope.getCustomerOrders = function (mobile) {
        crudService.get("Order/GetCustomerOrders?mobile=", mobile)
        .success(function (data) {
            angular.forEach(data, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
                item.LastUpdate = parseDate(item.LastUpdate);
            });
            $scope.customerOrders = data;
            $("#CustomerOrders").modal("show");
        })
    }

});
//.controller('crudOrderCtrl', ['$location', '$scope', '$routeParams', 'crudService', 'Authentication', function ($location, $scope, $routeParams, crudService, Authentication) {
//    Authentication.authorize('QuanLy, NhanVien');
//    $scope.create = function (data) {
//        $("#myModal").modal("show");
//        crudService.get("/SIM/GetByNumber?number=", data.Number)
//            .success(function (sim) {
//                data.SIM_ID = sim.ID;
//                data.Price = sim.Price;
//                var currentUser = Authentication.currentUser();
//                data.CreateBy = currentUser.Name;
//                data.UserBussiness = currentUser.Name;
//                crudService.create("/Order/Create", data)
//                    .success(function (data) {
//                        console.log(data);
//                        $("#myModal").modal("hide");
//                        $location.path("/giao-dich/")
//                    })
//                    .error(function (error) {
//                        $("#myModal").modal("hide");
//                        console.log(error);
//                    });
//            })
//            .error(function (error) {
//                $("#myModal").modal("hide");
//                alert("SIM ko tồn tại!")
//                console.log(error);
//            })
//    }
//    // update
//    //var id = $routeParams.id;
//    //if (id) {
//    //    crudService.get("/Order/Get/", id)
//    //        .success(function (data) {
//    //            data.CreateDate = parseDate(data.CreateDate);
//    //            data.LastUpdate = parseDate(data.LastUpdate);
//    //            $scope.orderUpdate = data;
//    //        })
//    //        .error(function (error) {
//    //            console.log(error);
//    //        });
//    //}
//    //$scope.update = function (data) {
//    //    $("#myModal").modal("show");
//    //    crudService.get("/SIM/GetByNumber?number=", data.Number)
//    //        .success(function (sim) {
//    //            data.SIM_ID = sim.ID;
//    //            data.Price = sim.Price;
//    //            var currentUser = Authentication.currentUser();
//    //            data.UpdateBy = currentUser.Name;
//    //            crudService.update("/Order/Update", data)
//    //                .success(function (data) {
//    //                    $("#myModal").modal("hide");
//    //                    $location.path("/giao-dich/")
//    //                })
//    //                .error(function (error) {
//    //                    $("#myModal").modal("hide");
//    //                    console.log(error);
//    //                });
//    //        })
//    //        .error(function (error) {
//    //            $("#myModal").modal("hide");
//    //            alert("SIM ko tồn tại.")
//    //            console.log(error);
//    //        })
//    //}

//    //delete
//    $scope.remove = function (id) {
//        crudService.remove("/Order/Delete", id)
//            .success(function (data) {
//                $location.path("/giao-dich/");
//            })
//            .error(function (error) {
//                console.log(error);
//            });
//    }
//    // function
//    var parseDate = function (value) {
//        if (value) {
//            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
//        }
//        return null;
//    }
//}]);