'use strict';

module.exports = Favorites;

Favorites.$inject = ['$http', '$q', 'operations', 'endpoints'];

function Favorites($http, $q, operations, endpoints) {
  this.$http = $http;
  this.$q = $q;
  this.operations = operations;
  this.endpoints = endpoints;
}

Favorites.prototype.addToFavorites = function(scriptId) {
  var self = this,
    result = this.$q.defer();

  if (this.operations.haveAuthenticationCredentials()) {
    this.$http.post(this.endpoints.getAddToFavoriteScriptsEndpoint(), {
      id: scriptId
    }).success(function() {
      self.operations.setFavorite(scriptId);
      result.resolve();
    });
  } else {
    this.operations.promptForSscCredentials();
    result.reject();
  }
  return result.promise;
};
