'use strict';

var angular = require('angular');
require('angular-route');
require('angular-hljs');

require('./controllers');
require('./directives');
require('./services');

var app = angular.module('sqlScripts', [
  'ngRoute',
  'sqlScripts.controllers',
  'sqlScripts.directives',
  'sqlScripts.services',
  'hljs'
]);

app.config(function($routeProvider, $locationProvider) {
    $routeProvider
      .when('/', {
            redirectTo: '/featured'
      })
      .when('/all', {
        templateUrl: '/views/scripts.html',
        controller: 'ScriptsController'
      })
      .when('/featured', {
        templateUrl: '/views/featured.html',
        controller: 'FeaturedController'
      })
      .when('/favorites', {
        templateUrl: '/views/favorites.html',
        controller: 'FavoritesController'
      })
      .when('/contribute', {
        templateUrl: '/views/contribute.html',
        controller: 'ContributeController'
      });
});
