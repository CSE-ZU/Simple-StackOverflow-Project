using GraphQL.Types;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserRegisterRequestType : InputObjectGraphType
{
    //request
    public UserRegisterRequestType()
    {
        Name = "userRegisterRequest";
        Field<NonNullGraphType<StringGraphType>>("username");
        Field<NonNullGraphType<StringGraphType>>("email");
        Field<NonNullGraphType<StringGraphType>>("password");
    }
}