/// <binding BeforeBuild='default' Clean='clean' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    del = require('del');

gulp.task('clean', function () {
    return del(['wwwroot/css/third_party', 'wwwroot/js/third_party']);
});

gulp.task('copyCss', function () {
    gulp.src(['bower_components/bootstrap/dist/css/bootstrap.min.css'])
        .pipe(gulp.dest('wwwroot/css/third_party'));
});

gulp.task('copyJs', function () {
    gulp.src(['bower_components/jquery/dist/jquery.min.js',
              'bower_components/bootstrap/dist/js/bootstrap.min.js',
              'bower_components/tether/dist/js/tether.min.js'])
        .pipe(gulp.dest('wwwroot/js/third_party'));
});

gulp.task('default', ['clean', 'copyCss', 'copyJs']);