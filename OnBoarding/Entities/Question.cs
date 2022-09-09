namespace OnBoarding.Entities;

public class Question
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }    
    public DateTime CreationTime { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public virtual ICollection<Answer> Answers { get; set; }
}