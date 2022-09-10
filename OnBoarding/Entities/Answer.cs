namespace OnBoarding.Entities;

public class Answer
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public DateTime CreationTime { get; set; }

    public Guid QuestionId { get; set; }
    public Question Question { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}