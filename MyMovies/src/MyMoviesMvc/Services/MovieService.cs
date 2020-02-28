using MyMoviesMvc.Context;
using MyMoviesMvc.DataModels;
using System.Collections.Generic;

namespace MyMoviesMvc.Services
{
    public class MoviesService
    {
        public MovieContext MovieContext { get; }

        public MoviesService(MovieContext movieContext)
        {
            MovieContext = movieContext;
        }

        public IEnumerable<MovieModel> Movies =>
            MovieContext.Movies;
    }
}
