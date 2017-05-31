angular.module('myApp').controller('AddKeywordModalController', [
    '$scope', '$uibModalInstance', 'KeywordResource','data',
    function ($scope, modalInstance, KeywordResource, keywordCategory) {


        $scope.addKeyword = function() {
            KeywordResource.add({ keyword: $scope.keyword, category: keywordCategory }, function(data) {
                console.log(data);
                modalInstance.close(data);
            });
        }

        $scope.dismiss = function() {
            modalInstance.dismiss();
        };

        console.log(modalInstance);

    }
]);