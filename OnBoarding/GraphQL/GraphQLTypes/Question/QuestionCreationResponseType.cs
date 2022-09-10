using GraphQL.Types;

namespace OnBoarding.GraphQL.GraphQLTypes.Question;

public class QuestionCreationResponseType : ObjectGraphType<Entities.Question>
{
    public QuestionCreationResponseType()
    {
        Field(x => x.Id);
        Field(x => x.CreationTime);
        Field(x => x.UserId);
        Field(x => x.Title);
        Field(x => x.Body);
    }
}