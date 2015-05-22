angular.module("sbAdmin")
.controller("supplierCtrl", function ($location, $scope, $routeParams, crudService, Authentication) {
    Authentication.authorize('QuanLy, NhanVien');
    $scope.lstSupplier = [];
    // init
    crudService.getAll("/Supplier/GetAll")
    .success(function (data) {
        //  console.log(data);
        angular.forEach(data, function (item) {
            item.CreateDate = parseDate(item.CreateDate);
            item.LastUpdate = parseDate(item.LastUpdate);
        });
        $scope.lstSupplier = data;
    })
    .error(function (error) {
        console.log(error);
    });
    //
    $scope.create = function (data) {
        $("#myModal").modal("show");
        var user = {};
        user.Email = data.Email;
        user.UserName = data.UserName;
        user.Password = data.Password;
        user.ConfirmPassword = data.ConfirmPassword;

        var currentUser = Authentication.currentUser();
        data.CreateBy = currentUser.Name;
        data.isActive = true;
        data.isDeleted = false;

        crudService.create("/Supplier/Create", { model: data, regModel: user })
        .success(function (data) {
            console.log(data);
            $("#myModal").modal("hide");
            $location.path("/quan-ly/dai-ly/")
        })
        .error(function (error) {
            $("#myModal").modal("hide");
            console.log(error);
        })
    }
    // update
    var id = $routeParams.id;
    if (id) {
        crudService.get("/Supplier/Get/", id)
        .success(function (data) {
            data.CreateDate = parseDate(data.CreateDate);
            data.LastUpdate = parseDate(data.LastUpdate);
            $scope.spUpdate = data;
            $scope.currentPassword = angular.copy(data.Password, $scope.currentPassword);
        })
        .error(function (error) {
            console.log(error);
        });
    }
    $scope.update = function (data) {
        $("#myModal").modal("show");
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        crudService.update("/Supplier/Update", { model: data, currentPassword: $scope.currentPassword })
        .success(function (data) {
            $("#myModal").modal("hide");
            $location.path("/quan-ly/dai-ly/");
        })
        .error(function (error) {
            $("#myModal").modal("hide");
            console.log(error);
        })
    }
    //delete
    $scope.remove = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        data.isActive = false;
        data.isDeleted = true;
        crudService.update("/Supplier/Update", data)
        .success(function (data) {
            $location.path("/quan-ly/dai-ly/");
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