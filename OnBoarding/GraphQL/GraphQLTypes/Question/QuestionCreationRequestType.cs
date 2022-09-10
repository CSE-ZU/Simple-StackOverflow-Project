using GraphQL.Types;

namespace OnBoarding.GraphQL.GraphQLTypes.Question;

public class QuestionCreationRequestType : InputObjectGraphType
{
    public QuestionCreationRequestType()
    {
        Name = "questionCreationRequest";
        Field<NonNullGraphType<StringGraphType>>("Title");
        Field<NonNullGraphType<StringGraphType>>("Body");
        Field<NonNullGraphType<StringGraphType>>("UserId");
    }
}