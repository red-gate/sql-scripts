'use strict';

var angular = require('angular');
require('angular-route');

require('./controllers');
require('./directives');
require('./services');

var app = angular.module('sqlScripts', [
  'ngRoute',
  'sqlScripts.controllers',
  'sqlScripts.directives',
  'sqlScripts.services'
]);

app.config(function($routeProvider, $locationProvider) {
  $routeProvider
      .when('/', {
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
