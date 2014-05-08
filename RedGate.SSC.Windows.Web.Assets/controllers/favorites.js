'use strict';

module.exports = FavoritesController;

FavoritesController.$inject = ['$scope', 'operations', 'scripts'];

function FavoritesController($scope, operations, scripts) {
  $scope.scripts = [];

  $scope.selectedScript = null;

  $scope.selectScript = function(script) {
    if (script !== $scope.selectedScript) {
      scripts.hydrateScript(script);
      $scope.selectedScript = script;
    }
  };

  $scope.open = function(scriptId) {
    scripts.openScript(scriptId);
  };

  $scope.loadPage = function() {
    $scope.loading.isLoading = true;
    scripts.loadFavorites($scope.favoritesInfo, function(scripts) {
      // Add class for rating
      scripts.forEach(function(script) {
        script.ratingClass = 'is-rated' + script.rating.toString().replace('.', '');
      });

      $scope.scripts = scripts;
      $scope.selectedScript = scripts[0];

      $scope.loading.isLoading = false;
      $scope.loading.pageLoaded = true;
    });
  };

  if (!operations.haveAuthenticationCredentials()) {
    operations.promptForSscCredentials();
    $scope.updateLoggedInUser();
  }

  $scope.loadPage();
}
