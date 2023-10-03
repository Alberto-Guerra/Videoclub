using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories.Interfaces;

public interface IUserRepository
{
    User? GetById(int id);
    User? GetByUserName(string userName);
    IEnumerable<User> GetAll();
    void addUser(User user);
}
