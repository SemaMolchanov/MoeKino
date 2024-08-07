namespace MoeKinoWebApp.Models;

public class Genre
{
    public int Id { get; set; }
    public string NameEn { get; set; }
    public string NameRu { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

}
