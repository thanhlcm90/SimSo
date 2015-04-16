angular.module("crud", [])
.service("crudService", function ($http) {
    this.getAll = function (url) {
        return $http.get(url);
    }

    this.get = function (url, id) {
        console.log(id);
        return $http.get(url + id);
    }

    this.create = function (url, data) {
        var request = $http({
            method: "post",
            url: url,
            data: data
        })
        return request;
    }

    this.update = function (url, data) {
        var request = $http({
            method: "post",
            url: url,
            data: data
        })
        return request;
    }

    this.remove = function (url, id) {
        var request = $http({
            method: "post",
            url: url + '/' + id
        })
        return request;
    }
})