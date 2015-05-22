angular.module("authenticate", [])
.factory("Authentication", function ($http,$location) {
    var currentUser = undefined;
    if (window.signedIn) {
        $http.get("/Account/GetCurrentUser")
        .success(function (data) {
         currentUser = data;
     });
    }

    var isInRoles= function(roles){
        return roles.indexOf(currentUser.Role)>-1;
    }

    var checkUserRole= function(roles){
       $http.get("/Account/GetCurrentUser")
       .success(function(data){
        if(roles.indexOf(data.Role)==-1){
            $location.path("/error");
        }
    })
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
    },
    authorize: function(roles){
        if(currentUser){
            if(!isInRoles(roles)){
               $location.path("/error");
           }
       }else{
        checkUserRole(roles);
    }
}
}
})