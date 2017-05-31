angular.module('myApp').factory('SentimentResource', ['$resource',
  function ($resource) {
      return $resource('/api/sentiment',
      {
          action: '@action',
          id: '@id'
      }, {
          get: { method: 'GET', params: { action: null, id: null }, isArray: false }
      });
  }]);
