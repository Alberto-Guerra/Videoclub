namespace Videoclub.API.Model;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string PhotoURL { get; set; }

    public bool Available { get; set; } = true;

    public Category Category { get; set; }

    public IEnumerable<RentHistory> RentHistories { get; set; }
}



public static class Constants
{
    public const string AVAILABLE_STRING = "Available";
    public const string NOT_AVAILABLE_STRING = "Rented";
}

