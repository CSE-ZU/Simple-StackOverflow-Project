using GraphQL.Types;
using OnBoarding.GraphQL.GraphQLTypes;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.GraphQL.GraphQLQueries;

public class AppQuery : ObjectGraphType
{
    public AppQuery(IUserRepository userRepository)
    {
        Field<ListGraphType<UserType>>(
            "users",
            resolve: context => userRepository.GetAll()
        );
    }
}