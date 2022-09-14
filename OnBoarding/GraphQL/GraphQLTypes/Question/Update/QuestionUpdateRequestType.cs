using GraphQL.Types;

namespace OnBoarding.GraphQL.GraphQLTypes.Question.Update;

public class QuestionUpdateRequestType : InputObjectGraphType
{
    public QuestionUpdateRequestType()
    {
        Name = "questionUpdateRequest";
        Field<NonNullGraphType<StringGraphType>>("id");
        Field<NonNullGraphType<StringGraphType>>("Title");
        Field<NonNullGraphType<StringGraphType>>("Body");
    }
}