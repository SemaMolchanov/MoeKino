using MoeKinoWebApp.Models;
public class MovieListViewModel
{
    // Список фильмов с основными данными
    public List<MovieViewModel> Movies { get; set; }
    
    // Список жанров для фильтрации
    public List<Genre> Genres { get; set; }
    
    // Список стран для фильтрации
    public List<Country> Countries { get; set; }
    
    // Список годов выпуска для фильтрации
    public List<int> Years { get; set; }
}

public class MovieViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Poster { get; set; } // Base64 изображение постера
    public string Description { get; set; } // Описание фильма
    public int ReleaseYear { get; set; }
    public List<Genre> Genres { get; set; } // Список жанров
    public List<Country> Countries { get; set; } // Список стран
}