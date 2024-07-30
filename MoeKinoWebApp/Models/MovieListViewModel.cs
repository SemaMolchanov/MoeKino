namespace MoeKinoWebApp.Models;

public class MovieListViewModel
{
    public string GenreName { get; set; }
    public List<MovieDetail> Movies { get; set; }
}

public class MovieDetail
{
    public int Id { get; set; }
    public int ReleaseYear { get; set; }
    public string Title { get; set; }
    public string Poster { get; set; } // Base64 Image
}
