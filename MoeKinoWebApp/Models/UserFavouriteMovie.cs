using Microsoft.Extensions.Configuration.UserSecrets;

namespace MoeKinoWebApp.Models;

public class UserFavouriteMovie
{
    public int UserID   { get; set; }
    public int MovieID { get; set; }

    public User User { get; set; }
    public Movie Movie { get; set; }
}