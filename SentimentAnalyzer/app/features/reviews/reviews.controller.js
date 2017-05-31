angular.module('myApp').controller('ReviewsController', [
    '$scope', 'ReviewsResource', 'SentimentResource',
    function ($scope, ReviewsResource, SentimentResource) {

        $scope.reviewsOverview = null;

        $scope.sentimentResult = null;

        $scope.overviewLoading = false;
        $scope.sentimentLoading = false;

        $scope.getSentiment = function () {
            $scope.sentimentLoading = true;
            SentimentResource.get({ appId: $scope.applicationId }, function(data) {
                $scope.sentimentResult = data;
                $scope.sentimentLoading = false;
            });
        };

        $scope.getReviews = function () {
            $scope.overviewLoading = true;
            $scope.sentimentResult = null;
            ReviewsResource.getOverview({ appId: $scope.applicationId }, function (data) {
                $scope.reviewsOverview = data;
                $scope.overviewLoading = false;
            });
        };
    }
]);