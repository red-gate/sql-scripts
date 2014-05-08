'use strict';

var angular = require('angular');

angular.module('sqlScripts.directives', [])
  .directive('menuItem', require('./menu-item'));
