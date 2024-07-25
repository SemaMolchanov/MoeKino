namespace MoeKinoWebApp.Models;


public class Movie
{
    public int Id { get; set; }

    public int ReleaseYear {get; set;}

    public string TitleEn { get; set; }

    public string TitleRu { get; set; }

    public string DescriptionEn {get; set;}

    public string DescriptionRu {get; set;}

    public string TrailerLinkEn {get; set;}

    public string TrailerLinkRu {get; set;}
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<MovieImage> MovieImages { get; set; } = new List<MovieImage>();
    public ICollection<MovieParticipant> MovieParticipants { get; set; } = new List<MovieParticipant>();
}