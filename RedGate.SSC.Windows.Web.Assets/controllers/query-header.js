'use strict';

module.exports = QueryHeaderController;

QueryHeaderController.$inject = ['$scope', 'scriptItem', 'favorites'];

function QueryHeaderController($scope, scriptItem, favorites) {
  $scope.title = scriptItem.title;
  $scope.author = scriptItem.author;
  $scope.addToFavoritesVisible = true;
  $scope.discussUrl = 'http://www.sqlservercentral.com/FindForumThread/'  + scriptItem.contentItemId;

  $scope.addToFavorites = function() {
    favorites.addToFavorites(scriptItem.contentItemId).then(function() {
      $scope.addToFavoritesVisible = false;
    });
  };
}
