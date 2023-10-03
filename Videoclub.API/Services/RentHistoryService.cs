using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class RentHistoryService : IRentHistoryService
{
    private IRentHistoryRepository _historyRepository;
    private IUserRepository _userRepository;

    public RentHistoryService(
        IRentHistoryRepository historyRepository,
        IUserRepository userRepository
    )
    {
        _historyRepository = historyRepository;
        _userRepository = userRepository;
    }

    public IEnumerable<RentHistory> GetByMovieId(int id)
    {
        return _historyRepository.GetByMovieId(id);
    }

    public DateTime? GetRentDateFromMovieId(int id)
    {
        RentHistory? history = _historyRepository.GetRentedByMovieId(id);
        if (history == null)
        {
            return null;
        }
        return history.RentDate;
    }

    public int? GetUserIdFromMovieId(int id)
    {
        RentHistory? history = _historyRepository.GetRentedByMovieId(id);
        if (history == null)
        {
            return null;
        }
        return history.UserId;
    }

    public string? GetUsernameFromMovieId(int id)
    {
        RentHistory? history = _historyRepository.GetRentedByMovieId(id);
        if (history == null)
        {
            return null;
        }
        User? user = _userRepository.GetById(history.UserId);
        if (user == null)
        {
            return null;
        }
        return user.Username;
    }
}
