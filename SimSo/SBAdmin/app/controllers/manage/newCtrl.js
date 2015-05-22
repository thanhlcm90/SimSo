angular.module("sbAdmin")
.controller("lstNewCtrl", function ($location, $scope, $routeParams, crudService, $http,Authentication) { 
  Authentication.authorize('QuanLy, NhanVien');

    //model
    $scope.lstNew = [];
    $scope.lstMenu = [];
    $scope.lstPage = [];
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.pageCount = 0;
    $scope.totalItems = 0;
    $scope.currentItems = 0;
    $scope.filterMenuId = 0;
    // get data from server
    var getData = function (index, pageSize) {
        return $http({
            url: "/New/GetByMenu",
            method: "GET",
            params: { menuID: $scope.filterMenuId, pageIndex: index, pageSize: pageSize }
        });
    }
    // init
    var init = function () {
        getData(1, $scope.pageSize)
        .success(function (data) {
            angular.forEach(data.Items, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
            });
            $scope.lstNew = data.Items;
            $scope.pageCount = data.PageCount;
            $scope.totalItems = data.TotalItems;
            $scope.currentItems = data.Items.length;
            for (i = 1; i <= $scope.pageCount && i <= 9; i++) {
                $scope.lstPage.push(i);
            }
        })
        .error(function (error) {
            console.log(error);
        })
    }
    init();

    //get list menu
    crudService.getAll("/Menu/GetAll")
    .success(function (data) {
     data.reverse();
     $scope.lstMenu = data;
     $scope.lstMenu.push({ ID: 0, Name: "Tất cả" });
     $scope.lstMenu.reverse();
 })
    .error(function (error) {
     console.log(error);
 })
    // change page size
    $scope.changePageSize = function () {
        $scope.currentPage = 1;
        $scope.lstPage.splice(0);
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
        getData(index, $scope.pageSize)
        .success(function (data) {
            angular.forEach(data.Items, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
            });
            $scope.lstNew = data.Items;
            $scope.currentItems = data.Items.length;
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
})

.controller("crudNewCtrl", function ($scope, $routeParams, $location, crudService, Authentication, $http) {
  Authentication.authorize('QuanLy, NhanVien');
    $scope.lstMenu = [];
    //
    $scope.options = {
        language: 'vi',
        allowedContent: true,
        entities: false,
    };
    //get list menu
    crudService.getAll("/Menu/GetAll")
    .success(function (data) {
     $scope.lstMenu = data;
 })
    .error(function (error) {
     console.log(error);
 })
    //
    $scope.create = function (data) {
        $("#myModal").modal("show");
        var currentUser = Authentication.currentUser();
        data.CreateBy = currentUser.Name;
        data.isActive = true;
        data.isDeleted = false;
        var files = $("#chooseFile").get(0).files;
        if (!files[0]) {
            crudService.create("/New/Create", data)
            .success(function (result) {
               $("#myModal").modal("hide");
               $location.path("/quan-ly/tin-tuc/")
           })
            .error(function (error) {
               $("#myModal").modal("hide");
               console.log(error);
           })
        } else {
            uploadFile()
            .success(function (result) {
                data.image = result;
                crudService.create("/New/Create", data)
                .success(function (result) {
                    $("#myModal").modal("hide");
                    $location.path("/quan-ly/tin-tuc/")
                })
                .error(function (error) {
                    $("#myModal").modal("hide");
                    console.log(error);
                })
            })
        }

    }
    //update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/New/Get/", id)
        .success(function (data) {
            data.CreateDate = parseDate(data.CreateDate);
            data.LastUpdate = parseDate(data.LastUpdate);
            $scope.newUpdate = data;
            var isHaveImg = data.image != null;
            if (isHaveImg) {
                angular.element("#imageUpload").show();
            }
        })
        .error(function (error) {
            alert(error);
        });
    }

    $scope.update = function (data) {
        $("#myModal").modal("show");
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        var files = $("#chooseFile").get(0).files;
        if (!files[0]) {
            crudService.update("/New/Update", data)
            .success(function (data) {
                $location.path("/quan-ly/tin-tuc/");
            })
            .error(function (error) {
                console.log(error);
            })
        } else {
            uploadFile()
            .success(function (result) {
                data.image = result;
                crudService.update("/New/Update", data)
                .success(function (result) {
                    $("#myModal").modal("hide");
                    $location.path("/quan-ly/tin-tuc/")
                })
                .error(function (error) {
                    $("#myModal").modal("hide");
                    console.log(error);
                })
            })
        }

    }
    //delete
    $scope.remove = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.isActive = false;
        data.isDeleted = true;
        crudService.update("/New/Update", data)
        .success(function (data) {
         $location.path("/quan-ly/tin-tuc/");
     })
        .error(function (error) {
         console.log(error);
     })
    }

    var uploadFile = function () {
        var formData = new FormData();
        var files = $("#chooseFile").get(0).files;
        formData.append("photo", files[0]);
        return $http.post("/Helper/UploadFile?name=photo", formData, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });
    }
    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
})