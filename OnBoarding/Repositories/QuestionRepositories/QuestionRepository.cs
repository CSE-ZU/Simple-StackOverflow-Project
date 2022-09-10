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

    public async Task<Question> GetQuestionById(Guid id)
    {
        return await _context.Questions.FindAsync(id);
    }
    public async Task<IEnumerable> GetAllQuestions()
    {
        return await _context.Questions.ToListAsync();
    }
    
    public async Task<Question> CreateQuestion(Question question)
    {
        question.Id = Guid.NewGuid();
        question.CreationTime = DateTime.UtcNow;
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return question;
    }
}