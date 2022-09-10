using GraphQL.Types;
using OnBoarding.Entities;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserType : ObjectGraphType<User>
{
    public UserType(IUserRepository userRepository)
    {
        Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the user object.");
        Field(x => x.UserName).Description("UserName property from the user object.");
        Field(x => x.Email).Description("Email property from the user object.");
    }
}