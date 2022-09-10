
using GraphQL.Server;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Authorization;
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddScoped<AppSchema>();
builder.Services.AddGraphQL(options =>
    {

    })
    .AddSystemTextJson()
    .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped);

builder.Services.Configure<JwtTokenConfig>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    // add altair UI to development only
    app.UseGraphQLPlayground();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseGraphQL<AppSchema>();
app.UseGraphQLPlayground(options: new GraphQL.Server.Ui.Playground.PlaygroundOptions());

app.Run();