app.factory('AuthorizationService', ['$http', '$q', '$resource', 'localStorageService', function ($http, $q, $resource, localStorageService) {

    var serviceBase = '/api';

    var authServiceResouce = $resource(serviceBase + "/token", null,
    {
        requestToken: { method: 'POST', isArray: false, headers: { "Content-Type": "application/json" } }
    });

    //var saveRegistration = function (registration) {

    //    return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
    //        return response;
    //    });

    //};

    var login = function (email, password) {

        var userDto = {
            email: email,
            password: password
        };

        var deferred = $q.defer();

        authServiceResouce.requestToken(userDto, function (data) {

            localStorageService.set('authorizationData', JSON.stringify({ token: data.token }));

            deferred.resolve(data);
        }, function (data) {
            deferred.reject(data);
        });

        return deferred.promise;

    };

    //var logOut = function () {

    //    localStorageService.remove('authorizationData');

    //    _authentication.isAuth = false;
    //    _authentication.userName = "";

    //};

    //var fillAuthData = function () {

    //    var authData = localStorageService.get('authorizationData');
    //    if (authData) {
    //        _authentication.isAuth = true;
    //        _authentication.userName = authData.userName;
    //    }

    //};

    //authServiceFactory.saveRegistration = _saveRegistration;
    //authServiceFactory.login = _login;
    //authServiceFactory.logOut = _logOut;
    //authServiceFactory.fillAuthData = _fillAuthData;
    //authServiceFactory.authentication = _authentication;

    return {
        login: function (email, password) {
            return login(email, password);
        }
    }
}]);