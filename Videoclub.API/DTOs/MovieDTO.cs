using Microsoft.AspNetCore.Mvc;
using Videoclub.API.Model;

namespace Videoclub.API.DTOs;
public class MovieDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string PhotoURL { get; set; }

    public string State { get; set; }

    public string Category { get; set; }

    public DateTime? RentDate { get; set; }

    public string? UsernameRented { get; set; }

    public int? UserId { get; set; }


}
