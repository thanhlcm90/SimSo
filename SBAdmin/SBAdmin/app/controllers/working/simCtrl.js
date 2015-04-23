angular.module("sbAdmin")
.controller("getSIMCtrl", function ($scope, crudService, $location,$http) {
    $scope.lstSIM = [];
    $scope.lstPage = [];
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    $scope.pageCount = 0;
    // get data from server
    var getData = function (index, itemsPerPage) {
        return $http({
            url: "/SIM/GetPageSim",
            method: "GET",
            params: { pageIndex: index, itemsPerPage: itemsPerPage }
        });
    }
    // init
    var init = function () {
        getData(1, $scope.itemsPerPage)
            .success(function (data) {
                $scope.lstSIM = data.ListSim;
                $scope.pageCount = data.Count;
                for (i = 1; i <= $scope.pageCount && i <= 9; i++) {
                    $scope.lstPage.push(i);
                }
            })
            .error(function (error) {
                console.log(error);
            })
    }

    init();

    $scope.changeItemPerPage = function () {
        init();
    }
    // select page
    $scope.selectPage = function (index) {
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
        getData(index, $scope.itemsPerPage)
            .success(function (data) {
                $scope.lstSIM = data.ListSim;
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
})
.controller("crudSIMCtrl", function ($scope, crudService, $http, $routeParams, $location, Authentication) {
    // models
    $scope.lstSimType = [];
    $scope.lstNetwork = [];
    $scope.lstSupplier = [];
    $scope.SIM = {};
    //init
    crudService.getAll("/NetWork/GetAll")
        .success(function (data) {
            $scope.lstNetwork = data;
        })
        .error(function (error) {
            console.log(error);
        })

    crudService.getAll("/SimType/GetAll")
        .success(function (data) {
            $scope.lstSimType = data;
        })
        .error(function (error) {
            console.log(error);
        })
    crudService.getAll("/Supplier/GetAll")
        .success(function (data) {
            $scope.lstSupplier = data;
        })
        .error(function (error) {
            console.log(error);
        })
    // tự động nhận loại nhà mạng và loại sim
    $scope.changeNumber = function () {
        if ($scope.SIM.Number && ($scope.SIM.Number.length >= 3)) {
            var number = $scope.SIM.Number;
            //get nw id
            var leng = $scope.lstNetwork.length;
            for (i = 0; i < leng; i++) {
                item = $scope.lstNetwork[i];
                if (item.Number.indexOf(number.substring(0, 3)) > -1 || item.Number.indexOf(number.substring(0, 4)) > -1) {
                    $scope.SIM.NetWork_ID = item.ID;
                    break;
                } else {
                    $scope.SIM.NetWork_ID = 0;
                }
            }
            //get simtype id
            if ($scope.SIM.Number.length >= 10) {
                crudService.get("/SimType/GetByNumber?number=", number)
                    .success(function (data) {
                        $scope.SIM.SimType_ID = data;
                    })
                    .error(function (error) {
                        $scope.SIM.SimType_ID = 0;
                        console.log(error);
                    });
            } else {
                $scope.SIM.SimType_ID = 0;
            }
        } else {
            $scope.SIM.NetWork_ID = 0;
            $scope.SIM.SimType_ID = 0;
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
                $location.path("/sim/")
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
                $location.path("/sim/")
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
                $location.path("/sim/")
            })
            .error(function (error) {
                console.log(error);
            })
    }

    //
    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
})
.controller("importSIM", function ($scope, $http) {
    $scope.upload = function () {
        $("#myModal").modal("show");
        var formData = new FormData();
        var files = $("#chooseFile").get(0).files;
        formData.append("excel", files[0]);
        $http.post("/SIM/UploadFile", formData, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        }).success(function (data) {
            $("#myModal").modal("hide");
            alert(data);
        }).error(function (error) {
            $("#myModal").modal("hide");
            alert("Something wrong!");
            console.log(error);
        })
    }
})
.controller("checkSIM", function ($scope, $http) {
    $scope.filter = {};
    $scope.filter.itemsPerPage = 10;
    $scope.lstSIM = [];
    $scope.lstPage = [];
    $scope.currentPage = 1;
    $scope.pageCount = 0;

    // kiem tra click
    $scope.check = function (filter, index) {
        $scope.currentPage = index;
        $http({
            url: "/SIM/GetSIMsByNumber",
            method: "GET",
            params: { number: filter.number, pageIndex: index, itemsPerPage: filter.itemsPerPage }
        })
            .success(function (data) {
                $scope.lstSIM = data.ListSim;
                $scope.pageCount = data.Count;
                $scope.lstPage.splice(0);
                for (i = 1; i <= $scope.pageCount && i <= 9; i++) {
                    $scope.lstPage.push(i);
                }
            })
            .error(function (error) {
                console.log(error);
            })
    }

    $scope.selectPage = function (index) {
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
        $http({
            url: "/SIM/GetSIMsByNumber",
            method: "GET",
            params: { number: $scope.filter.number, pageIndex: index, itemsPerPage: $scope.filter.itemsPerPage }
        })
        .success(function (data) {
            $scope.lstSIM = data.ListSim;
        })
    }

    $scope.getClass = function (index) {
        return $scope.currentPage == index ? "active" : "";
    }

    $scope.isDisable = function (index1, index2) {
        return index1 == index2 ? "disabled" : "";
    }
})