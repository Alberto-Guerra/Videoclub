namespace Videoclub.API.DTOs;

public class RentHistoryDTO
{
    public DateTime RentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
}
