'use strict';

module.exports = LoginController;

LoginController.$inject = ['$scope', '$http', '$log', 'endpoints', 'credentials'];

function LoginController($scope, $http, $log, endpoints, credentials) {
  $scope.username = '';
  $scope.password = '';

  $scope.incorrectPasswordVisible = false;
  $scope.inputControlsEnabled = true;

  $scope.loginEnabled = function () {
    return $scope.username.length > 0 && $scope.password.length > 0;
  };

  $scope.login = function () {
    $scope.inputControlsEnabled = 'disabled';

    $http.defaults.headers.common.Authorization = $scope.username + ':' + $scope.password;
    $http.get(endpoints.getIsAuthenticatedEndpoint()).success(function() {
      credentials.saveCredentialsToStore($scope.username, $scope.password);
      $scope.incorrectPasswordVisible = false;
      $scope.inputControlsEnabled = false;
    }).error(function() {
      $log.error('unable to reach the server for authentication');
      $scope.incorrectPasswordVisible = true;
      $scope.inputControlsEnabled = false;
      $http.defaults.headers.common = {};
    })
  };
}
