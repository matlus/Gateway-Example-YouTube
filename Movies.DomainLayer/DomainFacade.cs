using Movies.DomainLayer.Managers;
using Movies.DomainLayer.Managers.DomainModels;
using Movies.DomainLayer.Managers.Enums;
using System.Collections.Generic;

namespace Movies.DomainLayer
{
    public sealed class DomainFacade
    {
        private readonly MovieManager movieManager;
        private MovieManager MovieManager { get { return movieManager; } }

        public DomainFacade()
        {
            movieManager = new MovieManager();
        }
        
        public IEnumerable<Movie> GetMoviesByGenre(Genre genre)
        {
            return MovieManager.GetMoviesByGenre(genre);
        }
        public IEnumerable<Movie> GetMoviesByYear(int year)
        {
            return MovieManager.GetMoviesByYear(year);
        }
    }
}
