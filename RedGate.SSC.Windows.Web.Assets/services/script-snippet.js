'use strict';

// Return C# proxy if available
if (window.RedGate_SSC_Windows_Client_ScriptSnippets) {
  module.exports = function() {
    return window.RedGate_SSC_Windows_Client_ScriptSnippets;
  };
  return;
}

module.exports = function () {
  return {
    getSnippet: function (guid) {
      return 'SELECT * FROM ' + guid;
    }
  };
};