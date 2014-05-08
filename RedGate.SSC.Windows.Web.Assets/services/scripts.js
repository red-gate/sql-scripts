'use strict';

module.exports = Scripts;

Scripts.$inject = ['$http', '$q', 'endpoints', 'operations'];

function Scripts($http, $q, endpoints, operations) {
  this.numberOfItemsPerPage = 20;
  this.$http = $http;
  this.$q = $q;
  this.endpoints = endpoints;
  this.operations = operations;
}

Scripts.prototype.loadScriptsForPage = function(index, callback, searchQuery) {
  var query = this.endpoints.getListScriptsEndpoint() + index,
      self = this;

  if (searchQuery) {
    query += '&q=' + searchQuery;
  }

  // Cancel ongoing request
  if (this.request) {
    this.request.resolve();
  }
  this.request = this.$q.defer();

  this.$http({ method: 'GET', url: query, timeout: this.request })
  .success(function(data) {
    var mappedScripts = data['Scripts'].map(function(item) {
      return {
        id: item['ContentItemId'],
        title: item['Title'],
        description: item['Description'],
        author: item['Authors'],
        createdDate: item['CreatedDate'],
        downloads: item['ViewsCount'],
        rating: item['Rating'],
        category: item['CategoryTag'],
        scriptText: 'Loading...'
      };
    });

    if (mappedScripts.length > 0) {
      self.hydrateScript(mappedScripts[0]); // make sure the first script gets downloaded
    }

    callback(mappedScripts, data['NumberOfPages']);
  });
};

Scripts.prototype.loadFavorites = function(favoritesInfo, callback) {
  var self = this;

  // Cancel ongoing request
  if (this.request) {
    this.request.resolve();
  }
  this.request = this.$q.defer();

  this.$http({ method: 'GET', url: this.endpoints.getFavoriteScriptsEndpoint(), timeout: this.request })
  .success(function(data) {
    var mappedScripts = data.map(function(item) {
      var favorited = new Date(favoritesInfo[item['ContentItemId']]);
      var updated = new Date(item['LastModifiedDate']) > favorited;
      // Mark as seen
      if (updated) {
        self.operations.setFavorite(item['ContentItemId']);
      }
      return {
        id: item['ContentItemId'],
        title: item['Title'],
        description: item['Description'],
        author: item['Authors'],
        createdDate: item['CreatedDate'],
        downloads: item['ViewsCount'],
        rating: item['Rating'],
        category: item['CategoryTag'],
        scriptText: 'Loading...',
        updated: updated
      };
    });

    if (mappedScripts.length > 0) {
      self.hydrateScript(mappedScripts[0]); // make sure the first script gets downloaded
    }

    callback(mappedScripts);
  });
};

Scripts.prototype.hydrateScript = function (script) {
  this.$http.get(this.endpoints.getSqlScriptEndpoint(script['id']))
  .success(function(data) {
    script.scriptText = data['Script']['SqlScript'];

    /*$('.highlight').each(function(i, e) {
          hljs.highlightBlock(e);
      });*/
  });
};

Scripts.prototype.openScript = function(scriptId) {
  this.operations.openSscQuery('' + scriptId);
};

Scripts.prototype.submitScript = function(script) {
  return this.$http.post(this.endpoints.getShareEndpoint(), {
    Title: script.title,
    AuthorEmail: this.operations.getAuthenticationCredentials()[0],
    Description: script.description,
    Text: script.description,
    SqlCode: script.content,
    PrimaryTag: 't-sql' //TODO: This isn't respected on the server, but is queried from the form
  });
}
