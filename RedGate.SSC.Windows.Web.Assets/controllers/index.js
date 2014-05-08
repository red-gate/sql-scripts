'use strict';

var angular = require('angular');

angular.module('sqlScripts.controllers', [])
  .controller('BaseController', require('./base'))
  .controller('ScriptsController', require('./scripts'))
  .controller('FavoritesController', require('./favorites'))
  .controller('ContributeController', require('./contribute'))
  .controller('QueryHeaderController', require('./query-header'))
  .controller('LoginController', require('./login'));
