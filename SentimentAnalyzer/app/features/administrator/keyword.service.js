angular.module('myApp').factory('KeywordResource', ['$resource',
  function ($resource) {
      return $resource('/api/keyword/:id',
      {
          id: '@id'
      }, {
          getAll: { method: 'GET', params: { id: null }, isArray: true },
          add: { method: 'POST', params: { id: null }, isArray: false },
          remove: { method: 'DELETE', params: { id: '@id' }, isArray: false }
      });
  }]);
