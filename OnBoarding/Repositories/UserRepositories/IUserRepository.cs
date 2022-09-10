using OnBoarding.Entities;

namespace OnBoarding.Repositories.UserRepositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User Register(User user);

    // User Login(string email, string password);
    User GetUserById(string id);
}