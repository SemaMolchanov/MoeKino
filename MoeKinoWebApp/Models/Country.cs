namespace MoeKinoWebApp.Models;

public class Country
{
    public int Id { get; set; }
    public string NameEn { get; set; }
    public string NameRu { get; set; }
    public ICollection<MovieCountry> MovieCountries { get; set; } = new List<MovieCountry>();
}