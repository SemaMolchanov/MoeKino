namespace MoeKinoWebApp.Models;

public class MovieParticipant
{
    public int MovieID { get; set;}
    public int PersonID { get; set;}
    public int MovieParticipantCategoryID { get; set;}

    public Movie Movie { get; set; } 
    public Person Person { get; set; }
    public MovieParticipantCategory MovieParticipantCategory { get; set; }
}