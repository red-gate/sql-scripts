'use strict';

// Return C# proxy if available
if (window.RedGate_SSC_Windows_Client_CredentialsFormViewModel) {
  module.exports = function() {
    return window.RedGate_SSC_Windows_Client_CredentialsFormViewModel;
  };
  return;
}

module.exports = Credentials;

Credentials.$inject = ['$log'];

function Credentials($log) {
  return {
    saveCredentialsToStore: function(user, pass) {
      $log.info('saving credentials to store ' + user + ' ' + pass);
    }
  };
};
