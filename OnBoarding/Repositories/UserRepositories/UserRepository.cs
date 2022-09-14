using System.ComponentModel.DataAnnotations;
using OnBoarding.Authorization;
using OnBoarding.Entities;
using OnBoarding.Helper;

namespace OnBoarding.Repositories.UserRepositories;


public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtUtils _jwtUtils;

    public UserRepository(ApplicationDbContext context, IJwtUtils jwtUtils)
    {
        _context = context;
        _jwtUtils = jwtUtils;
    }

    
    public IEnumerable<User> GetAll() => _context.Users.ToList();

    public User GetUserById(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public User Register(User user)
    {
        // validate
        if (string.IsNullOrWhiteSpace(user.Password))
            throw new Exception("Password is required");
        
        if (_context.Users.Any(x => x.Email == user.Email))
            throw new Exception("Email '" + user.Email + "' is already taken");
        
        if (_context.Users.Any(x => x.UserName == user.UserName))
            throw new Exception("Username '" + user.UserName + "' is already taken");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public LoginResponse Login(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new Exception("Username or password is incorrect");
        var response = new LoginResponse();
        response.Id = user.Id;
        response.UserName = user.UserName;
        response.Token = _jwtUtils.GenerateToken(user);
        return response;
    }
}