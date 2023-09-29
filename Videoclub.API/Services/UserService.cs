using Microsoft.EntityFrameworkCore;
using Videoclub.API.Context;
using Videoclub.API.DTOs;
using Videoclub.API.Model;
using Videoclub.API.Services.Interfaces;

namespace Videoclub.API.Services;

public class UserService : IUserService
{

    private VideoclubContext _context { get; set; }

    public UserService(VideoclubContext dbContext)
    {
        _context = dbContext;
    }

    //returns all the users in the database
    public IEnumerable<User> getAllUsers()
    {
        return _context.Users.ToList();
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

        _context.Users.Add(user);
        _context.SaveChanges();
    }


    //returns the user with the username passed as parameter or null in case no user is found
    public User getUserByUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidOperationException("Empty Username");
        }

        if (!UserExist(username))
        {
            throw new InvalidOperationException("Username does not exist");
        }

        return _context.Users.First(u => u.Username == username);
    }

    //checks if there is a user with the given username
    public bool UserExist(string username) {
        return _context.Users.Any(u => u.Username == username);
    }

    //checks if there is a user with the given id
    public bool UserExist(int id)
    {
        return _context.Users.Any(u => u.Id == id);
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
