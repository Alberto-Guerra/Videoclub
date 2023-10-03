using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories.Interfaces;

public interface IRentHistoryRepository
{
    void Add(RentHistory history);
    bool CheckRented(int movie_id);
    RentHistory? GetRentedByMovieId(int movie_id);
    void Update(RentHistory history);
    IEnumerable<RentHistory> GetByMovieId(int movie_id);
    IEnumerable<RentHistory> GetByUserId(int movie_id);
}
