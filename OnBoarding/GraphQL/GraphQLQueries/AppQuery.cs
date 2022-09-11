using GraphQL.Types;
using OnBoarding.Authorization;
using OnBoarding.GraphQL.GraphQLTypes;
using OnBoarding.GraphQL.GraphQLTypes.Question;
using OnBoarding.Repositories.QuestionRepositories;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.GraphQL.GraphQLQueries;


public class AppQuery : ObjectGraphType
{
    public AppQuery(IUserRepository userRepository)
    {

        Field<ListGraphType<UserType>>(
            "users",
            resolve: context =>
            {
                return userRepository.GetAll();
            }
        );

    }
    
    
}