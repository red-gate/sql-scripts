
FeaturedController.$inject = ['$scope', '$http', '$log', 'scripts'];

function FeaturedController($scope, $http, $log, scripts) {
    $scope.loading.isLoading = true;

    //This is a little bit of a hack. We are about to send a request to an external service, make sure we don't send auth
    var auth = $http.defaults.headers.common.Authorization;
    delete $http.defaults.headers.common.Authorization;

    $http.get('http://www.red-gate.com/products/sql-server-central/plugin/featured').success(function (data) {
        $scope.loading.isLoading = false;
        $scope.loading.pageLoaded = true;

        $scope.scripts = data['scripts'];
    }).error(function() {
        $log.error('unable to get featured scripts');
        $scope.loading.isLoading = false;
        $scope.loading.pageLoaded = true;
    }).finally(function() {
        $http.defaults.headers.common.Authorization = auth; //Re-add auth for subsequent visits to SSC
    });

    $scope.open = function (scriptId) {
        scripts.openScript(scriptId);
    };
}

module.exports = FeaturedController;
