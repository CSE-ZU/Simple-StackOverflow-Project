using GraphQL.Types;
using OnBoarding.Entities;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserRegisterResponseType : ObjectGraphType<User>
{
    //response
    public UserRegisterResponseType()
    {
        Field(x => x.Id).Description("id property from the user object.");
        Field(x => x.Email).Description("email property from the user object");
        Field(x => x.UserName).Description("username property from the user object");
    }
}