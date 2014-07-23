
FeaturedController.$inject = ['$scope', '$http', '$log'];

function FeaturedController($scope, $http, $log) {
    $scope.loading.isLoading = true;
    $http.get('https://regardtest.blob.core.windows.net/temp/Foo.json').success(function (data) {
        $scope.loading.isLoading = false;
        $scope.loading.pageLoaded = true;

        $scope.scripts = data['scripts'];
    }).error(function() {
        $log.error('unable to get featured scripts');
        $scope.loading.isLoading = false;
        $scope.loading.pageLoaded = true;
    });
}

module.exports = FeaturedController;
