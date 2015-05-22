angular.module("sbAdmin")
.controller("getSIMCtrl", function ($scope, crudService, $location, $http, Authentication) {
    Authentication.authorize('QuanLy, NhanVien');

    $scope.lstSIM = [];
    $scope.lstPage = [];
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.pageCount = 0;
    $scope.currentSimCount = 0;
    $scope.totalSim = 0;
    $scope.included = -1;
    //$scope.checked = false;
    // get data from server
    var getData = function (index, pageSize) {
        return $http({
            url: "/SIM/GetPageSim",
            method: "GET",
            params: { pageIndex: index, pageSize: pageSize, included: $scope.included == -1 ? null : $scope.included }
        });
    }

    // init
    var init = function () {
        $("#myModal").modal("show");
        getData(1, $scope.pageSize)
        .success(function (data) {
            $scope.lstSIM = data.Items;
            $scope.pageCount = data.PageCount;
            $scope.totalSim = data.TotalItems;
            $scope.currentSimCount = data.Items.length;
            //$scope.checked = false;
            //for (i = 0; i < $scope.currentSimCount ; i++) {
            //    if (data.Items[i].Status == 1) {
            //        $scope.checked = true;
            //        break;
            //    }
            //}
            //setChecked();
            for (i = 1; i <= $scope.pageCount && i <= 9; i++) {
                $scope.lstPage.push(i);
            }
            $("#myModal").modal("hide");
        })
        .error(function (error) {
            console.log(error);
        })
    }
    init();

    //var setChecked = function () {
    //    if ($scope.checked) {
    //        angular.element("#duyetPage").val("Bỏ duyệt cả trang");
    //    } else {
    //        angular.element("#duyetPage").val("Duyệt cả trang");
    //    }
    //}
    // 
    $scope.reload = function () {
        $scope.currentPage = 1;
        $scope.lstPage.splice(0);
        init();
    }
    // select page
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
        getData(index, $scope.pageSize)
        .success(function (data) {
            $scope.lstSIM = data.Items;
            $scope.currentSimCount = data.Items.length;
            //$scope.checked = false;
            //for (i = 0; i < $scope.currentSimCount ; i++) {
            //    if (data.Items[i].Status == 1) {
            //        $scope.checked = true;
            //        break;
            //    }
            //}
            //setChecked();
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

    $scope.duyet = function (id, status) {
        crudService.get("/SIM/Get/", id)
        .success(function (data) {
            data.Status = status ? 1 : 0;
            data.CreateDate = parseDate(data.CreateDate);
            data.UpdateBy = Authentication.currentUser().Name;
            data.LastUpdate = null;
            crudService.update("/SIM/Approve_Sim", data)
            .success(function () {
            })
            .error(function (err) {
                console.log(err);
            })
        })
    }

    $scope.duyetTrang = function () {
        $("#myModal").modal("show");
        // $scope.checked = !$scope.checked;
        // setChecked();
        var status = 1;
        crudService.update("/SIM/DuyetTrang", { page: $scope.currentPage, pageSize: $scope.pageSize, updateBy: Authentication.currentUser().Name, status: status, included: $scope.included == -1 ? null : $scope.included })
        .success(function () {
            if ($scope.included != -1) {
                $scope.currentPage = 1;
                $scope.lstPage.splice(0);
                init();
            }
            $(".cbk").prop("checked", "true")
            $("#myModal").modal("hide");
        })
        .error(function (err) {
            console.log(err);
            $("#myModal").modal("hide");
        })
    }

    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }

})
.controller("crudSIMCtrl", function ($scope, crudService, $routeParams, $location, Authentication) {
    Authentication.authorize('QuanLy, NhanVien');
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
.controller("importSIM", function ($scope, $http, Authentication, $location, crudService) {
    //Authentication.authorize('DaiLy');
    $scope.isShow = false;
    $scope.upload = function () {
        $("#myModal").modal("show");
        var formData = new FormData();
        var files = $("#chooseFile").get(0).files;
        if (files[0]) {
            formData.append("excel", files[0]);
            formData.append("supID", $scope.supID);
            $http.post("/SIM/UploadFile", formData, {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).success(function (data) {
                $("#myModal").modal("hide");
                alert(data);
            }).error(function (error) {
                $("#myModal").modal("hide");
                alert(error);
                console.log(error);
            })
        } else {
            $("#myModal").modal("hide");
            alert("Không tìm thấy file!")
        }
    }

    $scope.lstSupplier = [];
    // init
    crudService.getAll("/Supplier/GetAll")
    .success(function (data) {
        $scope.lstSupplier = data;
    })
    .error(function (error) {
        console.log(error);
    });

    $http.get("/Account/GetCurrentUser")
      .success(function (data) {
          if (data.Role != "DaiLy") {
              $scope.isShow = true;
          }
      })
})
.controller("checkSIM", function ($scope, $http, Authentication, $location) {
    $scope.filter = {};
    $scope.filter.pageSize = 10;
    $scope.lstSIM = [];
    $scope.lstPage = [];
    $scope.currentPage = 1;
    $scope.pageCount = 0;

    // kiem tra click
    $scope.check = function (filter, index) {
        $("#myModal").modal("show");
        $scope.currentPage = index;
        $http({
            url: "/SIM/GetSIMsByNumber",
            method: "GET",
            params: { number: filter.number, pageIndex: index, pageSize: filter.pageSize }
        })
        .success(function (data) {
            angular.forEach(data.Items, function (item) {
                item.LastUpdate = parseDate(item.LastUpdate);
            })
            $scope.lstSIM = data.Items;
            $scope.pageCount = data.PageCount;
            $scope.totalSim = data.TotalItems;
            $scope.lstPage.splice(0);
            for (i = 1; i <= $scope.pageCount && i <= 9; i++) {
                $scope.lstPage.push(i);
            }
            $("#myModal").modal("hide");
        })
        .error(function (error) {
            console.log(error);
            $("#myModal").modal("hide");
        })
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
        $http({
            url: "/SIM/GetSIMsByNumber",
            method: "GET",
            params: { number: $scope.filter.number, pageIndex: index, pageSize: $scope.filter.pageSize }
        })
        .success(function (data) {
            angular.forEach(data.Items, function (item) {
                item.LastUpdate = parseDate(item.LastUpdate);
            })
            $scope.lstSIM = data.Items;
            $("#myModal").modal("hide");
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
})