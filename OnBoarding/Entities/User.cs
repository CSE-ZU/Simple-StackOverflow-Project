namespace OnBoarding.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Question> Questions { get; set; }
    public ICollection<Answer> Answers { get; set; }
}