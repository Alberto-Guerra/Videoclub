using Videoclub.API.Context;
using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories;

public class RentHistoryRepository : IRentHistoryRepository
{
    private VideoclubContext _context;

    public RentHistoryRepository(VideoclubContext context)
    {
        _context = context;
    }

    public void Add(RentHistory history)
    {
        _context.Add(history);
        _context.SaveChanges();
    }

    public bool CheckRented(int movieId)
    {
        return _context.RentHistories.Any(x => x.MovieId == movieId && x.ReturnDate == null);
    }

    public IEnumerable<RentHistory> GetByMovieId(int movieId)
    {
        return _context.RentHistories.Where(h => h.MovieId == movieId).ToList();
    }

    public IEnumerable<RentHistory> GetByUserId(int userId)
    {
        return _context.RentHistories.Where(h => h.UserId == userId).ToList();
    }

    public RentHistory? GetRentedByMovieId(int movieId)
    {
        return _context.RentHistories.FirstOrDefault(
            x => x.MovieId == movieId && x.ReturnDate == null
        );
    }

    public void Update(RentHistory history)
    {
        _context.Update(history);
        _context.SaveChanges();
    }
}
