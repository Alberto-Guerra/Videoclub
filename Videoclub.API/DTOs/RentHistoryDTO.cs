namespace Videoclub.API.DTOs;

public class RentHistoryDTO
{
    public DateTime RentDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int movie_id { get; set; }

    public int user_id { get; set; }


}
