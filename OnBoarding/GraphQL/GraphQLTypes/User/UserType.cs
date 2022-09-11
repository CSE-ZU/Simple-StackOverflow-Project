using GraphQL.Types;
using OnBoarding.Entities;
using OnBoarding.GraphQL.GraphQLTypes.Question;
using OnBoarding.Repositories.QuestionRepositories;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserType : ObjectGraphType<User>
{
    public UserType(IQuestionRepository questionRepository)
    {
        Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the user.");
        Field(x => x.UserName).Description("The username of the user.");
        Field(x => x.Email).Description("The email of the user.");
        Field<ListGraphType<QuestionCreationResponseType>>(
            "Questions",
            resolve: context => questionRepository.GetQuestionByUserId(context.Source.Id)
        );
    }
}