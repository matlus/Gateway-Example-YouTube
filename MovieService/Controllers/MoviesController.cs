using Movies.DomainLayer;
using Movies.DomainLayer.Managers.DomainModels;
using Movies.DomainLayer.Managers.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MovieService.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        [Route("{genre}")]
        public IEnumerable<Movie> Get(string genre)
        {
            try
            {
                Genre movieGenre;
                Enum.TryParse<Genre>(genre, true, out movieGenre);
                return DomainFacade.GetMoviesByGenre(movieGenre);
            }
            catch (Exception e)
            {
                throw ConstructHttpResponseException("Parameter Genere = " + genre, e);                
            }
        }

        [HttpGet]
        [Route("{year:int}")]
        public IEnumerable<Movie> Get(int year)
        {
            try
            {
                return DomainFacade.GetMoviesByYear(year);
            }
            catch (Exception e)
            {
                throw ConstructHttpResponseException("Year = " + year.ToString(), e);
            }
        }
    }
}