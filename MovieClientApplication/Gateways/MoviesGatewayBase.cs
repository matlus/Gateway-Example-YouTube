using MovieClientApplication.Models;
using System;
using System.Collections.Generic;

namespace MovieClientApplication.Gateways
{
    internal abstract class MoviesGatewayBase : IDisposable
    {
        public IEnumerable<Movie> GetMoviesByGenre(Genre genre)
        {
            return GetMoviesByGenreCore(genre);
        }

        public IEnumerable<Movie> GetMoviesByYear(int year)
        {
            return GetMoviesByYearCore(year);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract IEnumerable<Movie> GetMoviesByGenreCore(Genre genre);
        protected abstract IEnumerable<Movie> GetMoviesByYearCore(int year);
        protected abstract void Dispose(bool disposing);
    }
}
