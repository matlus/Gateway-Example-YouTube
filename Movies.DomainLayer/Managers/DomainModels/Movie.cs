using Movies.DomainLayer.Managers.Enums;
using System.Runtime.Serialization;

namespace Movies.DomainLayer.Managers.DomainModels
{
    [DataContract]
    public sealed class Movie
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]        
        public string ImageUrl { get; set; }
        [DataMember]
        public Genre Genre { get; set; }
        [DataMember]
        public int Year { get; set; }

        public Movie()
        {
        }

        public Movie(string title, string imageUrl, Genre genre, int year)
        {
            Title = title;
            ImageUrl = imageUrl;
            Genre = genre;
            Year = year;
        }
    }
}
