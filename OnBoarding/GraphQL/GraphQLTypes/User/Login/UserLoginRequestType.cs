using GraphQL.Types;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserLoginRequestType : InputObjectGraphType
{
    //request
    public UserLoginRequestType()
    {
        Name = "userLoginRequest";
        Field<NonNullGraphType<StringGraphType>>("email");
        Field<NonNullGraphType<StringGraphType>>("password");
    }

}