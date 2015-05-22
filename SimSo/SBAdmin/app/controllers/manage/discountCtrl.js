angular.module('sbAdmin')
.controller('discountCtrl', ['$scope', 'crudService', 'Authentication', 'discountFac', function ($scope, crudService, Authentication, discountFac) {
    Authentication.authorize('QuanLy, NhanVien');
    $scope.lstDiscount = [];
    $scope.discount = {};
    $scope.action = "Danh sách";
    $scope.initial = function (supID) {
        discountFac.initial(supID);
    }

    $scope.getLstDiscount = function () {
        return discountFac.getLstDiscount();
    }

    $scope.preCreate = function () {
        delete $scope.discount;
        $scope.action = "Thêm mới";
        $("#createForm").slideDown();
        $("#btnSubmit").show();
        $("#btnUpdate").hide();
    }

    $scope.create = function (data) {
        var currentUser = Authentication.currentUser();
        data.CreateBy = currentUser.Name;
        data.SupID = discountFac.getSupID();

        crudService.create("/Discount/Create", data)
		.success(function (data) {
		    var lstDiscount = discountFac.getLstDiscount();
		    data.CreateDate = parseDate(data.CreateDate);
		    lstDiscount.push(data);
		    $scope.action = "Danh sách";
		    $("#createForm").slideUp();
		})
		.error(function (error) {
		    console.log(error);
		});
    }

    $scope.preUpdate = function (data) {
        $scope.discount = angular.copy(data, $scope.discount);
        $scope.action = "Cập nhật";
        $("#btnSubmit").hide();
        $("#btnUpdate").show();
        $("#createForm").slideDown();
    }

    $scope.update = function (data) {
        var currentUser = Authentication.currentUser();
        data.UpdateBy = currentUser.Name;
        crudService.update("/Discount/Update", data)
		.success(function (result) {
		    var lstDiscount = [];
		    lstDiscount = angular.copy(discountFac.getLstDiscount(), lstDiscount);
		    var len = lstDiscount.length;
		    for (i = 0; i < len; i++) {
		        if (lstDiscount[i].ID == data.ID) {
		            lstDiscount[i] = data;
		            discountFac.setLstDiscount(lstDiscount);
		            break;
		        }
		    }
		    delete $scope.discount;
		    $scope.action = "Danh sách";
		    $("#createForm").slideUp();
		})
		.error(function (error) {
		    console.log(error);
		});
    }

    $scope.remove = function (id) {
        crudService.remove("/Discount/Delete", id)
		.success(function () {
		    $("#createForm").slideUp();
		    var lstDiscount = discountFac.getLstDiscount();
		    lstDiscount = lstDiscount.filter(function (element) {
		        return element.ID != id;
		    });
		    discountFac.setLstDiscount(lstDiscount);
		})
		.error(function (error) {
		    console.log(error);
		});
    }

    $scope.cancel = function () {
        $scope.action = "Danh sách";
        $("#createForm").slideUp();
    }

    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
}])
.factory('discountFac', ['crudService', 'Authentication', function (crudService, Authentication) {
    var lstDiscount = [];
    var supID = 0;
    var parseDate = function (value) {
        if (value) {
            return new Date(parseInt(value.replace("/Date(", "").replace(")/", "")));
        }
        return null;
    }
    return {
        initial: function (id) {
            crudService.getAll("/Discount/GetBySupId?supID=" + id)
			.success(function (data) {
			    $("#DiscountModal").modal("toggle");
			    angular.forEach(data, function (item) {
			        item.CreateDate = parseDate(item.CreateDate);
			        item.LastUpdate = parseDate(item.LastUpdate);
			    });
			    lstDiscount = data;
			    supID = id;
			})
			.error(function (err) {
			    console.log(err);
			});
        },
        getLstDiscount: function () {
            return lstDiscount;
        },
        setLstDiscount: function (data) {
            lstDiscount = data;
        },
        getSupID: function () {
            return supID;
        }
    }
}])