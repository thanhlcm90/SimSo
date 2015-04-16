﻿angular.module("sbAdmin")
.controller("networkCtrl", function ($http, $location, $scope, $routeParams, crudService, Authentication) {
    //model
    $scope.lstNetWork = [];
    crudService.getAll("/Network/GetListNetWork")
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
        uploadFile()
            .success(function (result) {
                data.image = result;
                crudService.create("/Network/Create", data)
                    .success(function (result) {
                        $("#myModal").modal("hide");
                        $location.path("/thiet-lap-danh-muc/nha-mang/")
                    })
                    .error(function (error) {
                        $("#myModal").modal("hide");
                        console.log(error);
                    })
            })
    }
    //update
    var id = $routeParams.nwID;
    if (id) {
        crudService.get("/Network/Get/", id)
            .success(function (data) {
                console.log(data);
                data.CreateDate = parseDate(data.CreateDate);
                data.LastUpdate = parseDate(data.LastUpdate);
                $scope.nwUpdate = data;
            })
            .error(function (error) {
                alert(error);
            });
    }

    $scope.update = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.isActive = true;
        data.isDeleted = false;
        crudService.update("/Network/Update", data)
            .success(function (data) {
                $location.path("/thiet-lap-danh-muc/nha-mang/");
            })
            .error(function (error) {
                console.log(error);
            })
    }
    //delete
    $scope.remove = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.isActive = false;
        data.isDeleted = true;
        crudService.update("/Network/Update", data)
       .success(function (data) {
           $location.path("/thiet-lap-danh-muc/nha-mang/");
       })
       .error(function (error) {
           console.log(error);
       })
    }

    var uploadFile = function () {
        var formData = new FormData();
        var files = $("#chooseFile").get(0).files;
        formData.append("photo", files[0]);
        return $http.post("/Network/UploadFile", formData, {
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