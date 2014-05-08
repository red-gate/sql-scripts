'use strict';

var angular = require('angular');

angular.module('sqlScripts.services', [])
  .service('endpoints', require('./endpoints'))
  .service('operations', require('./operations'))
  .service('scripts', require('./scripts'))
  .service('scriptSnippet', require('./script-snippet'))
  .service('favorites', require('./favorites'))
  .service('scriptItem', require('./script-item'))
  .service('credentials', require('./credentials'))
  .service('utils', require('./utils'));
