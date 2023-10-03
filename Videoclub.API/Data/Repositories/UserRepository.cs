using Videoclub.API.Context;
using Videoclub.API.Data.Repositories.Interfaces;
using Videoclub.API.Model;

namespace Videoclub.API.Data.Repositories;

public class UserRepository : IUserRepository
{
    private VideoclubContext _context;

    public UserRepository(VideoclubContext context)
    {
        _context = context;
    }

    public void addUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public User? GetByUserName(string userName)
    {
        return _context.Users.FirstOrDefault(x => x.Username == userName);
    }
}
