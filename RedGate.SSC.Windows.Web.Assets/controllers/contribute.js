'use strict';

ContributeController.$inject = ['$scope', 'operations', 'scripts', 'scriptSnippet'];

module.exports = ContributeController;

function ContributeController($scope, operations, scripts, scriptSnippet) {
  $scope.getParameterByName = function (name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results === null)
      return "";
    else
      return decodeURIComponent(results[1].replace(/\+/g, " "));
  };

  $scope.script = {
    title: '',
    content: '',
    description: '',
    category: ''
  };

  $scope.agreements = {
    copyright: false,
    termsAndConditions: false
  };

  $scope.submitScriptDisabled = function () {
    return $scope.script.content.length === 0 ||
      $scope.script.title.length === 0 ||
      $scope.script.description.length === 0 ||
      $scope.script.category.length === 0 ||
      !$scope.agreements.copyright ||
      !$scope.agreements.termsAndConditions ||            
      $scope.loading.isLoading;
  };

  $scope.scriptContributed = false;

  $scope.submitScript = function () {
    if (operations.haveAuthenticationCredentials()) {
      $scope.loading.isLoading = true;
      scripts.submitScript($scope.script)
      .success(function() {
        $scope.scriptContributed = true;
      }).then(function() {
        $scope.loading.isLoading = false;
      });
    } else {
      operations.promptForSscCredentials();
      $scope.updateLoggedInUser();
    }
  };

  var snippetId = $scope.getParameterByName('snippetId');

  if (snippetId) {
    var snippet = scriptSnippet.getSnippet(snippetId);
    $scope.script.content = snippet;
  }

  $scope.loading.pageLoaded = true;
}