using System.Security.Claims;
using OnBoarding.GraphQL;
using OnBoarding.Repositories.UserRepositories;

namespace OnBoarding.Authorization;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateToken(token);
        // var id = Guid.Parse(userId);
        if (userId != null)
        {
            var user = userRepository.GetUserById(Guid.Parse(userId));
            
            // attach user to context on successful jwt validation
            context.Items["User"] = user;
        }
        await _next(context);
    }
}