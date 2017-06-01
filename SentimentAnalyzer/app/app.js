var app = angular.module('myApp', ['ngRoute', 'ngResource', 'LocalStorageModule', 'ui.bootstrap']);

app.config(['$routeProvider', '$httpProvider', '$windowProvider', '$injector',
    function ($routeProvider, $httpProvider, $windowProvider, $injector) {
        $routeProvider
            .when('/reviews', {
                templateUrl: 'app/features/reviews/reviews.template.html',
                controller: 'ReviewsController'
            })

            .when('/login', {
                templateUrl: 'app/features/login/login.template.html',
                controller: 'LoginController'
            })

            .when('/admin', {
                templateUrl: 'app/features/administrator/admin.template.html',
                controller: 'AdminController'
            })

            .otherwise({
                redirectTo: '/reviews'
            });

        $httpProvider.interceptors.push('authInterceptorService');

    }]);

app.run(['$rootScope', function ($rootScope) {

    $rootScope.logOff = function() {
        localStorage.removeItem('ls.authorizationData');
    };

    $rootScope.isAuthenticated = function () {
        return localStorage.getItem('ls.authorizationData');
    };
}]);