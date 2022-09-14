using System.Security.Claims;
using System.Text;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnBoarding.Authorization;
using OnBoarding.GraphQL;
using OnBoarding.GraphQL.GraphQLSchema;
using OnBoarding.Helper;
using OnBoarding.Repositories.AnswerRepositories;
using OnBoarding.Repositories.QuestionRepositories;
using OnBoarding.Repositories.UserRepositories;


var builder = WebApplication.CreateBuilder(args);
//register GraphQL

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(connectionString));

//register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ISchema, AppSchema>();
builder.Services.AddGraphQL(options =>
    {
        options.EnableMetrics = true;
    })
    .AddSystemTextJson()
    .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped)
    .AddGraphQLAuthorization(options =>
    {
        options.AddPolicy("AuthUsers", 
            policy => policy.RequireClaim(ClaimTypes.Role, "AuthUsers"));
    });

builder.Services.Configure<JwtTokenConfig>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground();
}

app.UseHttpsRedirection(); 

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.UseGraphQLPlayground(options: new GraphQL.Server.Ui.Playground.PlaygroundOptions());

app.Run();