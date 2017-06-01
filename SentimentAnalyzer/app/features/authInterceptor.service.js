angular.module('myApp').factory('authInterceptorService', ['$window',
    function ($window) {
        return {
            request: function (config) {

                if (config.url.indexOf('api') >= 0) {

                    var authData = $window.localStorage.getItem('ls.authorizationData');
                    if (authData) {
                        var authDataObject = JSON.parse(JSON.parse(authData));
                        config.headers.Authorization = 'Bearer ' + authDataObject.token;
                    }

                }

                return config;
            },
            responseError: function(config) {
                console.log(config.status);

                if (config.status === 401) {
                    $window.location.href = '#!/reviews';
                    $window.localStorage.removeItem('ls.authorizationData');
                }

                return config;
            }
        }
    }
]);