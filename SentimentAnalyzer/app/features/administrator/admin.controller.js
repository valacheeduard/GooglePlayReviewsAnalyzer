angular.module('myApp').controller('AdminController', [
    '$scope', 'KeywordResource', '$uibModal',
    function ($scope, KeywordResource, $uibModal) {

        $scope.keywordsList = [];

        KeywordResource.getAll({}, function (data) {
            console.log(data);
            $scope.keywordsList = data;
        });

        $scope.deleteKeyword = function(keywordId) {
            KeywordResource.remove({ id: keywordId }, function() {
                var index = null;

                $scope.keywordsList.forEach(function(item, i) {
                    if (item.id === keywordId) index = i;
                });

                if (index) {
                    $scope.keywordsList.splice(index, 1);
                }
            });
        };


        $scope.addKeyword = function (categoryCode) {
            $uibModal.open({
                templateUrl: '/app/features/administrator/addKeywordModal.template.html',
                controller: 'AddKeywordModalController',
                resolve: {
                    data: function () { return categoryCode }
                }
            }).result.then(function(newKeyword) {
                $scope.keywordsList.push(newKeyword);
            });
        }

    }
]);