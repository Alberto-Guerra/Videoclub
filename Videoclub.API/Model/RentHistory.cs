namespace Videoclub.API.Model;

public class RentHistory
{
    public DateTime RentDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int movie_id { get; set; }

    public Movie Movie { get; set; }

    public int user_id { get; set; }

    public User User { get; set; } 
}
