'use strict';

var gulp = require('gulp'),
  gutil = require('gulp-util'),
  uglify = require('gulp-uglify'),
  jade = require('gulp-jade'),
  concat = require('gulp-concat'),
  livereload = require('gulp-livereload'),
  tinylr = require('tiny-lr'),
  express = require('express'),
  app = express(),
  path = require('path'),
  server = tinylr(),
  imprt = require('rework-import'),
  rework = require('gulp-rework'),
  prefix = require('gulp-autoprefixer'),
  minifyCSS = require('gulp-minify-css'),
  modRewrite = require('connect-modrewrite'),
  browserify = require('gulp-browserify'),
  eventStream = require('event-stream');

// --- Basic Tasks ---
gulp.task('css', function() {
  return gulp.src('styles/main.css')
    .pipe(rework(imprt('styles')))
    .pipe(prefix('chrome >= 25'))
    .pipe(minifyCSS())
    .pipe(gulp.dest('dist'))
    .pipe(livereload(server));
});

gulp.task('images', function() {
  return gulp.src('images/*.svg')
    .pipe(gulp.dest('dist/images'));
});

gulp.task('js', function() {
  return eventStream.merge(
    gulp.src([
        'scripts/mocks.js'
      ])
      .pipe(gulp.dest('dist')),

    gulp.src('viewmodels/*.js')
      .pipe(gulp.dest('dist/viewmodels')),

    gulp.src('scripts/easter-egg/asteroids.js')
      .pipe(gulp.dest('dist/easter-egg')),

    gulp.src([
        'app.js'
      ])
      .pipe(browserify({
        shim: {
          angular: {
              path: 'scripts/angular.js',
              exports: 'angular'
          },
          'angular-route': {
              path: 'scripts/angular-route.js',
              exports: 'ngRoute',
              depends: {
                  angular: 'angular'
              }
          }
        }
      }))
      .pipe(concat('combined.js'))
      .pipe(gulp.dest('dist'))
      .pipe(livereload(server))
  );
});

gulp.task('templates', function() {
  return eventStream.merge(
    gulp.src('./views/!(index|login|query-header).jade')
      .pipe(jade({ pretty: true }))
      .pipe(gulp.dest('dist/views'))
      .pipe(livereload(server)),
    gulp.src('./views/{index,login,query-header}.jade')
      .pipe(jade({ pretty: true }))
      .pipe(gulp.dest('dist/'))
      .pipe(livereload(server))
  );
});

gulp.task('express', function() {
  app.use(require('connect-livereload')());
  app.use(modRewrite(['!^(/views|/images|/combined\.js|/main\.css) /index.html']));
  app.use(express.static(path.resolve('dist')));
  app.listen(1337);
  gutil.log('Listening on port: 1337');
});

gulp.task('watch', function () {
  server.listen(35729, function (err) {
    if (err) {
      console.log(err);
    }
  });

  gulp.watch('styles/*.css', ['css']);
  gulp.watch(['services/*.js', 'scripts/*.js', 'controllers/*.js', 'directives/*.js'], ['js']);
  gulp.watch('views/*.jade', ['templates']);
  gulp.watch('images/*.svg', ['images']);
});

// Default Task
gulp.task('default', ['js', 'css', 'templates', 'images', 'express', 'watch']);
