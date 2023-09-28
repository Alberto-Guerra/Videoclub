

using Videoclub.API.DTOs;
using Videoclub.API.Model;

namespace Videoclub.API.Services.Interfaces;

public interface IUserService
{
    void addUser(User user);
    User? getUserByUsername(string username);
    IEnumerable<User> getAllUsers();   

    bool UserExist(string username);
    bool UserExist(int id);

    void checkUserForRegister(UserDTO user);
}
