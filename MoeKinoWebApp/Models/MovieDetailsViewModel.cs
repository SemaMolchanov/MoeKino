using MoeKinoWebApp.Models;

public class MovieDetailsViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Poster { get; set; }
    public string TrailerLink { get; set; }
    public List<string> Genres { get; set; }
    public List<string> Countries { get; set; }
    public Dictionary<string, List<Person>> Participants { get; set; } // Ключ - категория, значение - список участников
    public Dictionary<int, string> Images { get; set; }
}
