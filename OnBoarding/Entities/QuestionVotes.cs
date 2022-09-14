namespace OnBoarding.Entities;

public class QuestionVotes
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid QuestionId { get; set; }
    public Question Question { get; set; }

    public Count Count { get; set; }
}