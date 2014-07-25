'use strict';

module.exports = menuItem;

menuItem.$inject = ['$rootScope', '$location'];

function menuItem($rootScope, $location) {
  return {
    restrict: 'A',
    link: function(scope, element, attrs) {
      var path = element.find('a').attr('href');
      $rootScope.$on('$locationChangeStart', function(event, next, current) {
        element.toggleClass('is-active', path.slice(1, path.length) === $location.path());
      });
    }
  };
}
