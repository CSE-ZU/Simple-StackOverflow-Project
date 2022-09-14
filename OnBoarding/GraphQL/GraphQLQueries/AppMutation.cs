using GraphQL;
using GraphQL.Types;
using OnBoarding.Entities;
using OnBoarding.GraphQL.GraphQLTypes;
using OnBoarding.GraphQL.GraphQLTypes.Question;
using OnBoarding.GraphQL.GraphQLTypes.Question.Update;
using OnBoarding.Repositories.QuestionRepositories;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.GraphQL.GraphQLQueries;

public class AppMutation : ObjectGraphType
{
    public AppMutation(IUserRepository userRepository, IQuestionRepository questionRepository)
    {
        
        Field<UserRegisterResponseType>(
            "Register",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<UserRegisterRequestType>> { Name = "user" }),
            resolve: context =>
            {
                var user = context.GetArgument<User>("user");
                
                return userRepository.Register(user);
            }
        );

        Field<UserLoginResponseType>(
            "Login",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<UserLoginRequestType>> { Name = "user" }),
            resolve: context =>
            {
                var user = context.GetArgument<User>("user");
                return userRepository.Login(user.Email, user.Password);
            }
        );
        
        Field<QuestionCreationResponseType>(
            "CreateQuestion",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<QuestionCreationRequestType>> { Name = "question" }),
            resolve: context =>
            {
                var question = context.GetArgument<Question>("question");
                var userContext = context.UserContext as GraphQLUserContext;
                var userId = Guid.Parse((ReadOnlySpan<char>)userContext.User.FindFirst("UserId").Value);
                return questionRepository.CreateQuestion(question, userId);
            }
        ).AuthorizeWith("AuthUsers");
        
        Field<QuestionUpdateResponseType>(
            "UpdateQuestion",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<QuestionUpdateRequestType>> { Name = "question" }),
            resolve: context =>
            {
                var question = context.GetArgument<Question>("question");
                var dbQuestion = questionRepository.GetQuestionById(question.Id);
                var userContext = context.UserContext as GraphQLUserContext;
                var userId = Guid.Parse((ReadOnlySpan<char>)userContext.User.FindFirst("UserId").Value);
                if (dbQuestion == null)
                {
                    context.Errors.Add(new ExecutionError("Question not found"));
                    return null;
                }
                if (dbQuestion.UserId != userId)
                {
                    context.Errors.Add(new ExecutionError("You are not allowed to update this question"));
                    return null;
                }
                return questionRepository.UpdateQuestion(question, userId, question.Id);
            }
        ).AuthorizeWith("AuthUsers");
        
        Field<StringGraphType>(
            "deleteQuestion",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "questionId" }),
            resolve: context =>
            {
                var questionId = context.GetArgument<Guid>("questionId");
                var question = questionRepository.GetQuestionById(questionId);
                if (question == null)
                {
                    context.Errors.Add(new ExecutionError("Couldn't find question in db."));
                    return null;
                }
                questionRepository.DeleteQuestion(question);
                return $"The question with the id: {questionId} has been successfully deleted from db.";
            }
        ).AuthorizeWith("AuthUsers");
    }
}