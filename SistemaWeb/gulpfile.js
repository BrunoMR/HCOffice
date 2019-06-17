var gulp            = require('gulp'),
    sass            = require('gulp-sass'),
    del             = require('del'),
    sourcemaps      = require('gulp-sourcemaps'),
    autoprefixer    = require('gulp-autoprefixer');

gulp.task('cleansass', function () {
    return del('Content/sass/dist/App.css');
});

//CLEAN
gulp.task('clean', ['cleansass']);
    
// SASS:
gulp.task('sass', function() {
    return gulp.src('Content/sass/App.scss')
        .pipe(sourcemaps.init())
        .pipe(sass().on('error', sass.logError))
        .pipe(autoprefixer({browsers: 'last 3 versions'}))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('Content/sass/dist'));
  });

// WATCH:
gulp.task('watch', function () {
    return gulp.watch(['Content/sass/**/*.scss'], ['sass']);
});

// INIT: 
gulp.task('default', ['clean', 'sass', 'watch']);


  