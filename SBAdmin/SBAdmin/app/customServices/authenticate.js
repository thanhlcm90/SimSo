angular.module("authenticate", [])
.factory("Authentication", function ($http) {
    var currentUser = undefined;
    return {
        signIn: function (signInUrl, signInUser) {
            return $http.post(signInUrl, signInUser)
        },
        signOut: function (signOutUrl) {
            return $http.post(signOutUrl);
        },
        getCurrentUser: function (url) {
            return $http.get(url);
        },
        signedIn: window.signedIn,
        setCurrentUser: function (data) {
            currentUser = data;
        },
        currentUser: function () {
            return currentUser;
        }
    }
})