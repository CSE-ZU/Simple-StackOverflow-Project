using GraphQL.Types;
using OnBoarding.Entities;

namespace OnBoarding.GraphQL.GraphQLTypes;

public class UserType: ObjectGraphType<User>
{
    public UserType()
    {
        Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the user.");
        Field(x => x.UserName).Description("The username of the user.");
        Field(x => x.Email).Description("The email of the user.");
    }
}