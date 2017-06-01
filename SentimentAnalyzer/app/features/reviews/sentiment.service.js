angular.module('myApp').factory('SentimentResource', ['$resource',
  function ($resource) {
      return $resource('/api/sentiment/:id/:action',
      {
          action: '@action',
          id: '@id'
      }, {
          get: { method: 'GET', params: { action: null, id: null }, isArray: false },
          download:{method: 'GET', params: { action: 'download', id: null }, isArray: false}
      });
  }]);
