
angular.module('myApp').controller('LoginController', [
    '$scope',  'AuthorizationService',
    function ($scope, AuthorizationService) {

        $scope.user = {
            email: null,
            password: null
        };
        //'administrator@test.com'
        // 'Admin123@'

        $scope.login = function () {
            AuthorizationService.login($scope.user.email, $scope.user.password);
        };
    }
]);