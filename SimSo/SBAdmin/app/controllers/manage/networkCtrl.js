angular.module("sbAdmin")
.controller("networkCtrl", function ($http, $location, $scope, $routeParams, crudService, Authentication) {
    //model
    $scope.lstNetWork = [];
    crudService.getAll("/Network/GetAll")
        .success(function (data) {
            angular.forEach(data, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
                item.LastUpdate = parseDate(item.LastUpdate);
            })
            $scope.lstNetWork = data;
        })
        .error(function (error) {
            console.log(error);
        })
    //create
    $scope.create = function (data) {
        $("#myModal").modal("show");
        var currentUser = Authentication.currentUser();
        data.CreateBy = currentUser.Name;
        data.isActive = true;
        data.isDeleted = false;
        var files = $("#chooseFile").get(0).files;
        if (!files[0]) {
            crudService.create("/Network/Create", data)
                .success(function (result) {
                    $("#myModal").modal("hide");
                    $location.path("/quan-ly/nha-mang/")
                })
                .error(function (error) {
                    $("#myModal").modal("hide");
                    console.log(error);
                })
        } else {
            uploadFile()
                .success(function (result) {
                    data.image = result;
                    crudService.create("/Network/Create", data)
                        .success(function (result) {
                            $("#myModal").modal("hide");
                            $location.path("/quan-ly/nha-mang/")
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
        crudService.get("/Network/Get/", id)
            .success(function (data) {
                data.CreateDate = parseDate(data.CreateDate);
                data.LastUpdate = parseDate(data.LastUpdate);
                $scope.nwUpdate = data;
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
            crudService.update("/Network/Update", data)
                .success(function (data) {
                    $location.path("/quan-ly/nha-mang/");
                })
                .error(function (error) {
                    console.log(error);
                })
        } else {
            uploadFile()
                .success(function (result) {
                    data.image = result;
                    crudService.update("/Network/Update", data)
                        .success(function (result) {
                            $("#myModal").modal("hide");
                            $location.path("/quan-ly/nha-mang/")
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
        crudService.update("/Network/Update", data)
       .success(function (data) {
           $location.path("/quan-ly/nha-mang/");
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
});