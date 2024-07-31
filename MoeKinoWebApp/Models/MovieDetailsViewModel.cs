using MoeKinoWebApp.Models;

public class MovieDetailsViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Poster { get; set; }
    public string TrailerLink { get; set; }
    public List<GenreViewModel> Genres { get; set; }
    public List<CountryViewModel> Countries { get; set; }
    public Dictionary<string, List<PersonViewModel>> Participants { get; set; } // Ключ - категория, значение - список участников
    public List<ImageViewModel> Images { get; set; }
}

public class GenreViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CountryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } 
}

public class PersonViewModel
{
    public int Id { get; set; }

    public DateOnly BirthDate{ get; set; }
    public string FullName { get; set; } 

    public string ShortBio { get; set; }
}

public class ImageViewModel
{
    public int Id { get; set; }
    public string Image { get; set; } 
}
