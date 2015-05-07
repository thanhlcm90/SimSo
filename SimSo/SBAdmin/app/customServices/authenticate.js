angular.module("authenticate", [])
.factory("Authentication", function ($http) {
    var currentUser = undefined;
    if (window.signedIn) {
        $http.get("/Account/GetCurrentUser")
       .success(function (data) {
           currentUser = data;
       });
    }
    return {
        signIn: function (signInUrl, signInUser) {
            return $http.post(signInUrl, signInUser)
        },
        signOut: function (signOutUrl) {
            return $http.post(signOutUrl);
        },
        signedIn: window.signedIn,
        currentUser: function () {
            return currentUser;
        }
    }
})