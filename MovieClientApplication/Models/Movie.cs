using System.Runtime.Serialization;

namespace MovieClientApplication.Models
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Movies.DomainLayer.Managers.DomainModels")]
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

        public override string ToString()
        {
            return
                "Title: " + Title + "\r\n" +
                "ImageUrl: " + ImageUrl + "\r\n" +
                "Genre: " + Genre.ToString() + "\r\n" +
                "Year: " + Year.ToString() + "\r\n";
        }
    }
}
