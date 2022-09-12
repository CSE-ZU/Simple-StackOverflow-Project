using System.Security.Claims;
using GraphQL.Authorization;

namespace OnBoarding.GraphQL;

public class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
{
    public ClaimsPrincipal User { get; set; }
    public GraphQLUserContext(ClaimsPrincipal user)
    {
        User = user;
    }
}