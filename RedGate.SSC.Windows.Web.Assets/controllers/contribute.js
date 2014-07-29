'use strict';

ContributeController.$inject = ['$scope', 'operations', 'scripts', 'scriptSnippet', '$routeParams'];

module.exports = ContributeController;

function ContributeController($scope, operations, scripts, scriptSnippet, $routeParams) {
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

  var snippetId = $routeParams['scriptId'];

  if (snippetId) {
    var snippet = scriptSnippet.getSnippet(snippetId);
    $scope.script.content = snippet;
  }

  $scope.loading.pageLoaded = true;
}