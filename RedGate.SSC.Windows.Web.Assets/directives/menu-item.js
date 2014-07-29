'use strict';

module.exports = menuItem;

menuItem.$inject = ['$rootScope', '$location'];

function menuItem($rootScope, $location) {
  return {
    restrict: 'A',
    link: function(scope, element, attrs) {
      var path = element.find('a').attr('href');
      $rootScope.$on('$locationChangeStart', function (event, next, current) {        
        var linkPath = path.slice(1, path.length);

        element.toggleClass('is-active', $location.path().substring(0, linkPath.length) === linkPath);
      });
    }
  };
}
