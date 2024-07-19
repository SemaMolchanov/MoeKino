namespace MoeKinoWebApp.Models;

public class MovieImage
{
    public int Id { get; set; }
    public byte[] Image { get; set; }
    public bool IsPoster { get; set; }
    public int MovieId { get; set; }

    // Navigation property
    public Movie Movie { get; set; }
}
