namespace MoeKinoWebApp.Models;


public class Person
{
    public int Id { get; set; }

    public DateOnly BirthDate {get; set;}

    public string FullNameEn { get; set; }

    public string FullNameRu { get; set; }

    public string ShortBioEn { get; set;}

    public string ShortBioRu { get; set;}
    public ICollection<MovieParticipant> MovieParticipants { get; set; } = new List<MovieParticipant>();
}