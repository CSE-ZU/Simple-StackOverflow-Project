using GraphQL.Types;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserInputType : InputObjectGraphType
{
    public UserInputType()
    {
        Name = "userInput";
        Field<NonNullGraphType<StringGraphType>>("username");
        Field<NonNullGraphType<StringGraphType>>("email");
        Field<NonNullGraphType<StringGraphType>>("password");
    }
}