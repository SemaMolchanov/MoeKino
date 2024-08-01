namespace MoeKinoWebApp.Models;
public class User
{

    public int Id { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; } // Хэшированный пароль

    public bool IsAdmin { get; set; }

    public ICollection<UserFavouriteMovie> UserFavouriteMovies { get; set; } = new List<UserFavouriteMovie>();
}
