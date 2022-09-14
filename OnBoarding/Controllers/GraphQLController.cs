using System.Text;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using GraphQL.Validation;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnBoarding.Authorization;
using OnBoarding.GraphQL;

namespace OnBoarding.Controllers;

[ApiController]
public class GraphQLController : ControllerBase
{
    private readonly ISchema _schema;
    private readonly IDocumentExecuter _documentExecutor;
    private readonly IDocumentWriter _documentWriter;
    private readonly IHostEnvironment _environment;
    private readonly ILogger<GraphQLController> _logger;
    public GraphQLController(
        ISchema schema, //replace with schema datatype
        IDocumentExecuter documentExecutor,
        IDocumentWriter documentWriter,
        IHostEnvironment environment,
        ILogger<GraphQLController> logger)
    {
        _schema = schema;
        _documentExecutor = documentExecutor;
        _documentWriter = documentWriter;
        _environment = environment;
        _logger = logger;
    }
    [Route("graphql")]
    [HttpPost]
    [Authorize]
    public async Task Post([FromBody] GraphQLQuery query, [FromServices] IEnumerable<IValidationRule> validationRules)
    {
        LogRequestDebug();
        GraphQLQuery(query);
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }
        var inputs = query.Variables.ToInputs();
        var executionOptions = new ExecutionOptions
        {
            Schema = _schema,
            Query = query.Query,
            Inputs = inputs,
            ValidationRules = validationRules
        };
        if (User.Identity?.IsAuthenticated ?? false)
        {
            if (HttpContext.Items.TryGetValue("GraphQLUserContext", out object? user) && user != null)
            {
                executionOptions.UserContext = user as GraphQLUserContext;
            }
        }
        executionOptions.UnhandledExceptionDelegate = context =>
        {
            _logger.LogError(context.Exception.Message);
            _logger.LogError(context.Exception.ToString()); // for Details.
        };
        var result = await _documentExecutor.ExecuteAsync(executionOptions).ConfigureAwait(false);
        if (result.Errors?.Count > 0)
        {
            Console.WriteLine(string.Join('\n', result.Errors.Select(e => e.Message)));
            HttpContext.Response.StatusCode = 400; // BadRequest
            if (_environment.IsDevelopment())
            {
                foreach (var error in result.Errors)
                {
                    await _documentWriter.WriteAsync(HttpContext.Response.Body, error.ToString());
                }
            }
            return;
        }
        HttpContext.Response.ContentType = "application/json";
        HttpContext.Response.StatusCode = 200; // OK
        await _documentWriter.WriteAsync(HttpContext.Response.Body, result);
    }
    
    
    [Route("register")]
    [HttpPost]
    [AllowAnonymous]
    public async Task Authenticate([FromBody] GraphQLQuery query, [FromServices] IEnumerable<IValidationRule> validationRules)
    {
        LogRequestDebug();
        GraphQLQuery(query);
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }
        var inputs = query.Variables.ToInputs();
        var executionOptions = new ExecutionOptions
        {
            Schema = _schema,
            Query = query.Query,
            Inputs = inputs,
            ValidationRules = validationRules
        };
        if (User.Identity?.IsAuthenticated ?? false)
        {
            if (HttpContext.Items.TryGetValue("GraphQLUserContext", out object? user) && user != null)
            {
                executionOptions.UserContext = user as GraphQLUserContext;
            }
        }
        executionOptions.UnhandledExceptionDelegate = context =>
        {
            _logger.LogError(context.Exception.Message);
            _logger.LogError(context.Exception.ToString()); // for Details.
        };
        var result = await _documentExecutor.ExecuteAsync(executionOptions).ConfigureAwait(false);
        if (result.Errors?.Count > 0)
        {
            Console.WriteLine(string.Join('\n', result.Errors.Select(e => e.Message)));
            HttpContext.Response.StatusCode = 400; // BadRequest
            if (_environment.IsDevelopment())
            {
                foreach (var error in result.Errors)
                {
                    await _documentWriter.WriteAsync(HttpContext.Response.Body, error.ToString());
                }
            }
            return;
        }
        HttpContext.Response.ContentType = "application/json";
        HttpContext.Response.StatusCode = 200; // OK
        await _documentWriter.WriteAsync(HttpContext.Response.Body, result);
    }

    private void LogRequestDebug()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Headers:");
        sb.AppendJoin("\n", Request.Headers.Select(kvp => $"\t {kvp.Key}: {kvp.Value.ToString()}"));
        _logger.LogDebug(sb.ToString());
    }
    private void GraphQLQuery(GraphQLQuery query)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"GQL Query:");
        sb.AppendLine($"\t OperationName: {query.OperationName}");
        sb.AppendLine($"\t Query: {query.Query}");
        _logger.LogInformation(sb.ToString());
    }
}
public class GraphQLQuery
{
    public string OperationName { get; set; } = "";
    public string NamedQuery { get; set; } = "";
    public string Query { get; set; } = "";
    public JObject Variables { get; set; } = new JObject();
}