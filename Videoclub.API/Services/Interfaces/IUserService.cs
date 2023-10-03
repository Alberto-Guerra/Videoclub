using Videoclub.API.DTOs;
using Videoclub.API.Model;

namespace Videoclub.API.Services.Interfaces;

public interface IUserService
{
    void addUser(User user, byte[] passwordHash, byte[] passwordSalt);
    User getUserByUsername(string username);
    User getUserById(int id);
    IEnumerable<User> getAllUsers();
    bool UserExist(string username);
    void checkUserForRegister(UserDTO user);
}
