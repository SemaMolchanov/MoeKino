namespace MoeKinoWebApp.Models;

public class MovieParticipantCategory
{
    public int Id { get; set; }
    public string NameEn { get; set; }
    public string NameRu { get; set; }
    public ICollection<MovieParticipant> MovieParticipants{ get; set; } = new List<MovieParticipant>();
}