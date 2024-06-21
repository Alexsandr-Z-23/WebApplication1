using System.Net.Http;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRouting(options =>
                options.ConstraintMap.Add("invalidnames", typeof(InvalidNamesConstraint)));
var app = builder.Build();

app.Map("/users/{name:invalidnames}", (string name) => $"Name: {name}");
app.Map("/", () => "Index Page");

app.Run();

public class InvalidNamesConstraint : IRouteConstraint
{
    string[] names = new[] { "Tom", "Sam", "Bob" };
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        return !names.Contains(values[routeKey]?.ToString());
    }
}