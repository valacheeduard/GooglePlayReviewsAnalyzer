
angular.module('myApp').controller('LoginController', [
    '$scope', 'AuthorizationService','$window',
    function ($scope, AuthorizationService, $window) {

        $scope.user = {
            email: null,
            password: null
        };
        //'administrator@test.com'
        // 'Admin123@'

        $scope.login = function () {
            AuthorizationService.login($scope.user.email, $scope.user.password).then(function() {
                $window.location.href = '#!/admin';
            });
        };
    }
]);