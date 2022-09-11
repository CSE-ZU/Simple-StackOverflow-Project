using OnBoarding.Entities;

namespace OnBoarding.Repositories.UserRepositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User Register(User user);

    User GetUserById(Guid id);
    
    LoginResponse Login(string email, string password);
}