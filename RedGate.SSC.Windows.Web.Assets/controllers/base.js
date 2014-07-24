'use strict';

module.exports = BaseController;

BaseController.$inject = ['$scope', '$http', '$timeout', 'operations', 'endpoints'];

function BaseController($scope, $http, $timeout, operations, endpoints) {
  // Auth
  $scope.auth = {
    username: '',
    isLoggedIn: false
  };

  $scope.updateLoggedInUser = function () {
    if (operations.haveAuthenticationCredentials()) {
      var creds = operations.getAuthenticationCredentials();
      $scope.auth.username = creds[0];
      $scope.auth.isLoggedIn = operations.haveAuthenticationCredentials();
      $http.defaults.headers.common.Authorization = creds[0] + ':' + creds[1];
    } else {
      $scope.auth.isLoggedIn = false;
      $scope.auth.username = '';
      delete $http.defaults.headers.common.Authorization;
    }
  };

  $scope.doLogin = function () {
    operations.promptForSscCredentials();
    $scope.updateLoggedInUser();
  };

  $scope.updateLoggedInUser();

  $scope.doLogout = function () {
    operations.deleteAuthenticationCredentials();
    $scope.updateLoggedInUser();
  };

  // Loading
  $scope.loading = {
    pageLoaded: false,
    isLoading: false
  };

  // Favorites updated
  $scope.favoritesUpdated = 0;
  $scope.favoritesInfo = JSON.parse(operations.getInfoOnFavorites());

  $timeout(function() {
    if ($scope.auth.isLoggedIn) {
      var updated = 0;

      $http.get(endpoints.getFavoriteScriptsEndpoint())
        .success(function(favorites) {
          favorites.forEach(function(favorite) {
            var favoritedDate = $scope.favoritesInfo[favorite.ContentItemId];

            // Not favorited within the tool
            if (!favoritedDate) {
              operations.setFavorite(favorite.ContentItemId);
            }

            // Check if updated
            if (new Date(favorite.LastModifiedDate) > new Date(favoritedDate)) {
              updated++;
            }
          });
          $scope.favoritesUpdated = updated;
        });
    }
  }, 2000);
}
