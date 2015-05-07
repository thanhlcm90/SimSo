angular.module("sbAdmin")
.controller("manageUserCtrl", function ($scope, $location, crudService, Authentication, $http, $routeParams) {
    // init
    if (Authentication.signedIn == "False") {
        window.location = "";
    }
    $scope.lstEmp = [];
    // crud
    $scope.create = function (user) {
        $("#myModal").modal("show");
        // insert to AspNetUser
        var data = {};
        data.Email = user.Email;
        data.UserName = user.UserName;
        data.Password = user.Password;
        data.ConfirmPassword = user.ConfirmPassword;
        crudService.create("/Account/CreateUser", { model: data, role: "NhanVien" })
            .success(function (result) {
            })
            .error(function (error) {
                console.log(error);
            });

        // insert to employee
        var emp = {};
        angular.copy(user, emp);
        emp.CreateBy = Authentication.currentUser().Name;
        emp.isActive = true;
        emp.isDeleted = false;
        delete emp.Password;
        delete emp.ConfirmPassword;
        var files = $("#chooseFile").get(0).files;
        if (!files[0]) {
            crudService.create("/Employee/Create", emp)
                 .success(function (result) {
                     $("#myModal").modal("hide");
                     $location.path('/quan-ly/nhan-vien');
                 })
                 .error(function (error) {
                     $("#myModal").modal("hide");
                     console.log(error);
                 });
        } else {
        uploadImage()
           .success(function (result) {
               emp.image = result;
               crudService.create("/Employee/Create", emp)
                    .success(function (result) {
                        $("#myModal").modal("hide");
                        $location.path('/quan-ly/nhan-vien');
                    })
                    .error(function (error) {
                        $("#myModal").modal("hide");
                        console.log(error);
                    });
           })
            .error(function (error) {
                alert("error");
            });
        }
    }


    // update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/Employee/Get/", id)
            .success(function (data) {
                data.CreateDate = parseDate(data.CreateDate);
                data.LastUpdate = parseDate(data.LastUpdate);
                data.BirthDay = parseDate(data.BirthDay);
                $scope.user = data;
                var isHaveImg = data.image != null;
                if (isHaveImg) {
                    angular.element("#imageUpload").show();
                }
            })
            .error(function (error) {
                alert(error);
            })
    }

    $scope.update = function (data) {
        $("#myModal").modal("show");
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        var files = $("#chooseFile").get(0).files;
        if (!files[0]) {
            crudService.update("/Employee/Update", data)
                .success(function (data) {
                    $location.path("/quan-ly/nhan-vien/");
                })
                .error(function (error) {
                    console.log(error);
                })
        } else {
            uploadImage()
                .success(function (result) {
                    data.image = result;
                    crudService.update("/Employee/Update", data)
                        .success(function (result) {
                            $("#myModal").modal("hide");
                            $location.path("/quan-ly/nhan-vien/")
                        })
                        .error(function (error) {
                            $("#myModal").modal("hide");
                            console.log(error);
                        })
                })
        }
    }
    // remove
    $scope.remove = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.isActive = false;
        data.isDeleted = true;
        crudService.update("/Employee/Update", data)
       .success(function (data) {
           $location.path("/quan-ly/nhan-vien");
       })
       .error(function (error) {
           console.log(error);
       })
    }

    //get all
    crudService.getAll("/Employee/GetAll")
        .success(function (emps) {
            angular.forEach(emps, function (item) {
                item.CreateDate = parseDate(item.CreateDate);
                item.LastUpdate = parseDate(item.LastUpdate);
                item.BirthDay = parseDate(item.BirthDay);
            })
            $scope.lstEmp = emps;
        })
       .error(function (error) {
       });
    // upload image
    var uploadImage = function () {
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