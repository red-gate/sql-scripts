'use strict';

// Return C# proxy if available
if (window.RedGate_SSC_Windows_Client_SscOperations) {
  module.exports = function() {
    return window.RedGate_SSC_Windows_Client_SscOperations;
  };
  return;
}

function Operations() {
  this.credentials = [];
}

Operations.prototype.getAuthenticationCredentials =  function () {
  return this.credentials;
};

Operations.prototype.haveAuthenticationCredentials = function () {
  return this.credentials.length > 0;
};

Operations.prototype.promptForSscCredentials = function () {
  this.credentials[0] = window.prompt('What is your username?');
  if (this.credentials[0] === 'a') {
      this.credentials[0] = 'aegir.thorsteinsson@red-gate.com';
  }
  this.credentials[1] = window.prompt('What is your password?');
};

Operations.prototype.deleteAuthenticationCredentials = function() {
  this.credentials = [];
};

Operations.prototype.getInfoOnFavorites = function() {
  return '{"106777": "2014-01-01","98476": "2014-01-01","107173": "2014-01-01","107287": "2014-01-01"}';
};

Operations.prototype.setFavorite = function(itemId) {
  return;
};

module.exports = Operations;
