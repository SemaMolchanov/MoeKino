namespace MoeKinoWebApp.Models;

public class MovieCarouselViewModel
{
    public string GenreName { get; set; }
    public List<MovieThumbnail> Movies { get; set; }
}

public class MovieThumbnail
{
    public int Id { get; set; }
    public int ReleaseYear { get; set; }
    public string Title { get; set; }
    public string Poster { get; set; } // Base64 Image
}
