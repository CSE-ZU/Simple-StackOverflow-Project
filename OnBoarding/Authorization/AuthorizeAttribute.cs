using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnBoarding.Entities;
using OnBoarding.GraphQL;

namespace OnBoarding.Authorization;

[AttributeUsage(AttributeTargets.All)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;
        // authorization
        var user = (User)context.HttpContext.Items["User"];

        if (user != null)
        {
            var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Role,"AuthUsers")
                },
                authenticationType:"Bearer");
            context.HttpContext.User = new ClaimsPrincipal(identity);
            context.HttpContext.Items["GraphQLUserContext"] = new GraphQLUserContext { User = context.HttpContext.User};
        }
        else
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) 
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
        
    }
}