
FeaturedController.$inject = ['$scope', '$http', '$log', 'scripts'];

function FeaturedController($scope, $http, $log, scripts) {
    $scope.loading.isLoading = true;
    $http.get('http://www.red-gate.com/products/sql-server-central/plugin/featured').success(function (data) {
        $scope.loading.isLoading = false;
        $scope.loading.pageLoaded = true;

        $scope.scripts = data['scripts'];
    }).error(function() {
        $log.error('unable to get featured scripts');
        $scope.loading.isLoading = false;
        $scope.loading.pageLoaded = true;
    });

    $scope.open = function (scriptId) {
        scripts.openScript(scriptId);
    };
}

module.exports = FeaturedController;
