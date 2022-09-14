using GraphQL.Types;

namespace OnBoarding.GraphQL.GraphQLTypes.Question.Update;

public class QuestionUpdateResponseType : ObjectGraphType<Entities.Question>
{
    public QuestionUpdateResponseType()
    {
        Field(x => x.Id, type: typeof(IdGraphType));
        Field(x => x.CreationTime);
        Field(x => x.UserId);
        Field(x => x.Title);
        Field(x => x.Body);
    }
}