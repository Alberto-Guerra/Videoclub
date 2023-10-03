namespace Videoclub.API.Model;

public class RentHistory
{
    public DateTime RentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
