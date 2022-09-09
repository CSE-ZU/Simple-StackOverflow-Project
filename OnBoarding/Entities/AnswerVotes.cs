namespace OnBoarding.Entities;

public class AnswerVotes
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid AnswerId { get; set; }
    public Answer Answer { get; set; }
    public Count Count { get; set; }
}
