'use strict';

module.exports = ScriptsController;
ScriptsController.$inject = ['$scope', 'utils', 'scripts', 'favorites'];

function ScriptsController($scope, utils, scripts, favorites) {
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

  $scope.searchText = '';

  $scope.$watch('searchText', utils.debounce(function() {
      $scope.loadPage(1);
  }, 500));

  $scope.loadPage = function(number) {
    $scope.loading.isLoading = true;
    scripts.loadScriptsForPage(number - 1, function(scripts, totalNumberOfPages) {
      // Add class for rating
      scripts.forEach(function(script) {
        script.ratingClass = 'is-rated' + script.rating.toString().replace('.', '');
        script.addToFavoritesVisible = true;
      });

      // List and selected item
      $scope.scripts = scripts;
      $scope.selectedScript = scripts[0];

      // Pages
      $scope.paging.current = number;
      $scope.paging.pageCount = totalNumberOfPages;

      $scope.loading.pageLoaded = true;
      $scope.loading.isLoading = false;
    }, $scope.searchText);
  };

  $scope.addToFavorites = function(script) {
    favorites.addToFavorites(script.id).then(function () {
      script.addToFavoritesVisible = false;
    });
  };

  // Paging
  $scope.paging = {
    current: 1,
    pageCount: 1
  };

  $scope.changePage = function() {
    $scope.loadPage(parseInt($scope.paging.current, 10));
  };

  $scope.prevPage = function() {
    $scope.paging.current = parseInt($scope.paging.current, 10) - 1;
  };
  $scope.nextPage = function() {
    $scope.paging.current = parseInt($scope.paging.current, 10) + 1;
  };

  $scope.$watch('paging.current', function(newPage) {
    if (newPage < 1) {
      $scope.paging.current = 1;
    }
    else if (newPage > $scope.paging.pageCount) {
      $scope.paging.current = $scope.paging.pageCount;
    }
    $scope.changePage();
  });
}
