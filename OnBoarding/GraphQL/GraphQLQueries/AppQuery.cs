using GraphQL;
using GraphQL.Types;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Authorization;
using OnBoarding.Entities;
using OnBoarding.GraphQL.GraphQLTypes;
using OnBoarding.GraphQL.GraphQLTypes.Question;
using OnBoarding.Repositories.QuestionRepositories;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.GraphQL.GraphQLQueries;

public class AppQuery : ObjectGraphType
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AppQuery(IUserRepository userRepository)
    {
        Field<ListGraphType<UserType>>(
            "users",
            resolve: context =>
            {
                return userRepository.GetAll();
            }
        );
        
        Field<UserType>(
            "user",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }),
            resolve: context =>
            {
                Guid id;
                if (!Guid.TryParse(context.GetArgument<string>("userId"), out id))
                {
                    context.Errors.Add(new ExecutionError("Wrong value for guid"));
                    return null;
                }
                return userRepository.GetUserById(id);
            }
        );

    }
    
    
}