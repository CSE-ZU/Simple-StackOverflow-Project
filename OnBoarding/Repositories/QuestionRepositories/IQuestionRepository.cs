using System.Collections;
using OnBoarding.Entities;

namespace OnBoarding.Repositories.QuestionRepositories;

public interface IQuestionRepository
{
    
    IEnumerable GetAllQuestions();
    IEnumerable<Question> GetQuestionsByUserId(Guid userId);
    Question GetQuestionById(Guid questionId);
    Question CreateQuestion(Question question, Guid userId);
    Question UpdateQuestion(Question question, Guid userId, Guid questionId);
    void DeleteQuestion(Question question);
    

}