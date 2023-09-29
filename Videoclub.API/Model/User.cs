namespace Videoclub.API.Model;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public DateTime Birthday { get; set; }

    public IEnumerable<RentHistory> RentHistories { get; set; }

}
