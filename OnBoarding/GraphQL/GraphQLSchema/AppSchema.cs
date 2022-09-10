using GraphQL.Types;
using OnBoarding.GraphQL.GraphQLQueries;

namespace OnBoarding.GraphQL.GraphQLSchema;

public class AppSchema : Schema
{
    public AppSchema(IServiceProvider provider)
        : base(provider)
    {
        Query = provider.GetRequiredService<AppQuery>();
        Mutation = provider.GetRequiredService<AppMutation>();
    }
}