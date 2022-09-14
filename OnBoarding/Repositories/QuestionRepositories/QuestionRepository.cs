using System.Collections;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Entities;
using OnBoarding.Helper;

namespace OnBoarding.Repositories.QuestionRepositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationDbContext _context;

    public QuestionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Vote(Guid questionId, Guid userId)
    {
        var questionVotes = _context.QuestionVotes.FirstOrDefault(qv => qv.QuestionId == questionId && qv.UserId == userId);
        if(questionVotes.CountType == CountType.Up)
        {
            questionVotes.CountType = CountType.Up;
        }
        else
        {
            questionVotes.CountType = CountType.Down;
        }
        _context.QuestionVotes.Update(questionVotes);
        _context.SaveChanges();
    }
    public IEnumerable GetAllQuestions()
    {
        return  _context.Questions.ToList();
    }
    public Question GetQuestionById(Guid id)
    {
        return  _context.Questions.Find(id);
    }
    
    public Question CreateQuestion(Question question, Guid userId)
    {
        question.CreationTime = DateTime.UtcNow;
        question.UserId = userId;
         _context.Questions.AddAsync(question);
         _context.SaveChangesAsync();
        return question;
    }
    
    public IEnumerable<Question> GetQuestionsByUserId(Guid userId)
    {
         var result =  _context.Questions.Where(q => q.UserId.Equals(userId)).ToList();
         return result;
    }
    
    public Question UpdateQuestion(Question question, Guid userId, Guid questionId)
    {
        var questionToUpdate = GetQuestionById(questionId);
        questionToUpdate.Title = question.Title;
        questionToUpdate.Body = question.Body;
        _context.Questions.Update(questionToUpdate);
        _context.SaveChanges();
        return questionToUpdate;
    }
    
    public int GetCount()
    {
        return  _context.Questions.Count();
    }
    public void DeleteQuestion(Question question)
    {
        _context.Questions.Remove(question);
        _context.SaveChanges();
    }
}