using System.Collections;
using OnBoarding.Entities;

namespace OnBoarding.Repositories.QuestionRepositories;

public interface IQuestionRepository
{
    Task<IEnumerable> GetAllQuestions();
    // IEnumerable<Question> GetQuestionByUserId(Guid userId);
    Task<Question> GetQuestionById(Guid questionId);
    Task<Question> CreateQuestion(Question question);
    // void UpdateQuestion(Question question);
    // void DeleteQuestion(Question question);
    // int GetQuestionCount();
    // int GetQuestionCountByUserId(Guid userId);
    
}