using GraphQL;
using GraphQL.Types;
using OnBoarding.Entities;
using OnBoarding.GraphQL.GraphQLTypes;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.GraphQL.GraphQLQueries;

public class AppMutation : ObjectGraphType
{
    public AppMutation(IUserRepository userRepository)
    {
        Field<UserRegisterResponseType>(
            "Register",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<UserRegisterRequestType>> { Name = "user" }),
            resolve: context =>
            {
                var user = context.GetArgument<User>("user");
                return userRepository.Register(user);
            }
        );

        Field<UserLoginResponseType>(
            "Login",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<UserLoginRequestType>> { Name = "user" }),
            resolve: context =>
            {
                var user = context.GetArgument<User>("user");
                return userRepository.Login(user.Email, user.Password);
            }
        );
    }
}