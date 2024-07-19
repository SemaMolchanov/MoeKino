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

}