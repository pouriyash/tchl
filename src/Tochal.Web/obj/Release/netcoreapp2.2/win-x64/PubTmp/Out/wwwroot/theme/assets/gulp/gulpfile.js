var gulp = require('gulp')
var sourcemaps = require('gulp-sourcemaps')
var sass = require('gulp-sass')

gulp.task('default', ['sass'], function() {
  gulp.watch('../scss/**/*.scss', ['sass'])
})

gulp.task('sass', function() {
  return gulp.src('../scss/**/*.scss')
  .pipe(sourcemaps.init())
  .pipe(sass().on('error', sass.logError))
  .pipe(sourcemaps.write())
  .pipe(gulp.dest('../styles'))
})
