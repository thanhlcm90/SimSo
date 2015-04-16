angular.module("waitingDialog", [])

.directive('waitingDialog', function () {
    return {
        restrict: 'E',
        scope: {
            content: '=content'
        },
        templateUrl: 'SBAmin/app/components/waitingDialog/dialog.html',
    };
});