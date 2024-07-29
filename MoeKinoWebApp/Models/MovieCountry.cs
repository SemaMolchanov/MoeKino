namespace MoeKinoWebApp.Models;
public class MovieCountry
{
    public int MovieID { get; set; }
    public int CountryID { get; set; }
    
    public Movie Movie { get; set; }
    public Country Country { get; set; }
}