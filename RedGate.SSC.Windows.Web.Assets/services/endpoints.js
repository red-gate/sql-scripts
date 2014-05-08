'use strict';

// Return C# proxy if available
if (window.SscEndpoints) {
  module.exports = function() {
    return window.SscEndpoints;
  };
  return;
}

function Endpoints() {
  this.apiRoot = 'https://www.sqlservercentral.com';
}

Endpoints.prototype.getSqlScriptEndpoint = function(id) {
  return this.apiRoot + '/api/scripts/' + id;
},

Endpoints.prototype.getIsAuthenticatedEndpoint = function () {
  return this.apiRoot + '/api/authenticate/';
},

Endpoints.prototype.getFavoriteScriptsEndpoint = function() {
  return this.apiRoot + '/api/briefcase/scripts';
},

Endpoints.prototype.getAddToFavoriteScriptsEndpoint = function() {
  return this.apiRoot + '/api/briefcase';
},

Endpoints.prototype.getRemoveFromFavoriteScriptsEndpoint = function() {
  return this.apiRoot + '/controls/briefcase/delete';
},

Endpoints.prototype.getShareEndpoint = function() {
  return this.apiRoot + '/api/scripts';
},

Endpoints.prototype.getListScriptsEndpoint = function() {
  return this.apiRoot + '/api/scripts?s=';
};

module.exports = Endpoints;
