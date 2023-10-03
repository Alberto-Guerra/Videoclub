using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    //returns all the users in the database
    public IEnumerable<User> getAllUsers()
    {
        return _userRepository.GetAll();
    }

    //returns the user with the id passed as parameter or throw an exception if it is not found
    public User getUserById(int id)
    {
        User? user = _userRepository.GetById(id);
        if (user == null)
        {
            throw new InvalidOperationException("User does not Exist");
        }
        return user;
    }

    //checks if the username is available, then saves the password hash and salt and adds the user to the database
    public void addUser(User user, byte[] passwordHash, byte[] passwordSalt)
    {
        if (UserExist(user.Username))
        {
            throw new InvalidOperationException(" User Already Exist ");
        }
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        _userRepository.addUser(user);
    }

    //returns the user with the username passed as parameter or throw an exception if it is not found or username is empty
    public User getUserByUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidOperationException("Empty Username");
        }
        User? user = _userRepository.GetByUserName(username);
        if (user == null)
        {
            throw new InvalidOperationException("User does not exist");
        }
        return user;
    }

    //checks if there is a user with the given username
    public bool UserExist(string username)
    {
        return _userRepository.GetByUserName(username) != null;
    }

    //checks if the user given have all the required fields and if the username is not already taken
    public void checkUserForRegister(UserDTO userDTO)
    {
        if (string.IsNullOrWhiteSpace(userDTO.Username))
        {
            throw new InvalidOperationException("Empty Username");
        }
        if (string.IsNullOrWhiteSpace(userDTO.Password))
        {
            throw new InvalidOperationException("Empty Password");
        }
        if (string.IsNullOrWhiteSpace(userDTO.Name))
        {
            throw new InvalidOperationException("Empty Name");
        }
        if (string.IsNullOrWhiteSpace(userDTO.LastName))
        {
            throw new InvalidOperationException("Empty LastName");
        }
        if (UserExist(userDTO.Username))
        {
            throw new InvalidOperationException("User already exists.");
        }
    }
}
