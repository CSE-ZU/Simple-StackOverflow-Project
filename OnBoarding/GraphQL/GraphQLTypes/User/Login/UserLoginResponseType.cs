using GraphQL.Types;
using OnBoarding.Entities;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserLoginResponseType : ObjectGraphType<LoginResponse>
{
    //response
    public UserLoginResponseType()
    {
        Field(x => x.Token).Description("JWT Token property from the user object.");
    }
}