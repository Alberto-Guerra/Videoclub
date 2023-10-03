using Videoclub.API.Model;

namespace Videoclub.API.Services.Interfaces;

public interface IRentHistoryService
{
    IEnumerable<RentHistory> GetByMovieId(int id);
    DateTime? GetRentDateFromMovieId(int id);
    int? GetUserIdFromMovieId(int id);
    string? GetUsernameFromMovieId(int id);
}
