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

    public IEnumerable<User> getAllUsers()
    {
        return _context.Users.ToList();
    }

    //adds the user to the database and saves the changes
    public void addUser(User user)
    {
        if (UserExist(user.Username))
        {
            throw new InvalidOperationException(" User Already Exist ");
        }

        _context.Users.Add(user);
        _context.SaveChanges();
    }


    //Returns the user with the username passed as parameter or null in case no user is found
    public User? getUserByUsername(string username)
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

    public bool UserExist(string username) {
        return _context.Users.Any(u => u.Username == username);
    }

    public bool UserExist(int id)
    {
        return _context.Users.Any(u => u.Id == id);
    }

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
