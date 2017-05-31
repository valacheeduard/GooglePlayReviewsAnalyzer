
angular.module('myApp').factory('ReviewsResource', ['$resource',
  function ($resource) {
      return $resource('/api/reviews',
      {
          action: '@action',
          id: '@id'
      }, {
          getOverview: { method: 'GET', params: { action: null, id: null }, isArray: false }
      });
  }]);
