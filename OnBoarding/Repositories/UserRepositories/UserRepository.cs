using OnBoarding.Entities;
using OnBoarding.Helper;

namespace OnBoarding.Repositories.UserRepositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<User> GetAll() => _context.Users.ToList();

    public User GetUserById(string id)
    {
        return _context.Users.SingleOrDefault(o => o.Id.Equals(id));
    }

    public User Register(User user)
    {
        // validate
        if (_context.Users.Any(x => x.Email == user.Email))
            throw new Exception("Email '" + user.Email + "' is already taken");

        if (_context.Users.Any(x => x.UserName == user.UserName))
            throw new Exception("Username '" + user.UserName + "' is already taken");


        user.Id = Guid.NewGuid();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
}