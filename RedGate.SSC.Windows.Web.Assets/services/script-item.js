'use strict';

// Return C# proxy if available
if (window.RedGate_SSC_Windows_Client_ScriptItem) {
  module.exports = function() {
    return window.RedGate_SSC_Windows_Client_ScriptItem;
  };
  return;
}

module.exports = function() {
  return {
    title: 'A Sample Title that has Some Long Text',
    author: "Aaron A. Aaronson",
    contentItemId: 98476,
    rating: 4
  };
};
