angular.module('myApp').controller('ReviewsController', [
    '$scope', '$http', 'ReviewsResource', 'SentimentResource',
    function ($scope, $http, ReviewsResource, SentimentResource) {

        $scope.reviewsOverview = null;

        $scope.sentimentResult = null;

        $scope.overviewLoading = false;
        $scope.sentimentLoading = false;

        $scope.downloadSentiment = function() {

            $http({
                url: '/api/sentiment/download?appId=' + $scope.applicationId,
                method: "GET",
                headers: {
                    'Content-type': 'application/json'
                },
                responseType: 'arraybuffer'
            }).then(function(response, status, headers, config) {
                console.log(response);
                var blob = new Blob([response.data], { type: "text/plain;charset=utf-8" });
                saveAs(blob, $scope.reviewsOverview.applicationName + " - Analysis.csv");

            },
            function (data, status, headers, config) {
                //upload failed
                console.log(data);
            });

            //SentimentResource.download({ appId: $scope.applicationId }, function(data) {
            //    console.log(data);
            //});
        }

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