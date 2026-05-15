# Whats DandyMediator?
Honestly, just yet another interpretation of the mediator pattern, based on the concepts of [JBogard's](https://github.com/jbogard) [MediatR](https://github.com/LuckyPennySoftware/MediatR). Just with a little of extra spice from my end, catered to my taste.

# Overview
## What can DandyMediator do?
DandyMediator should generally be able to do anything MediatR can and its API is largely the same, with exceptions like the naming of asynchronous methods (like `HandleAsync` instead of `Handle`), just to name an example. The configuration API however does not have that many options yet. Thats mostly because I have not needed anything other than scanning for services in assemblies yet.

However I added native markup-interfaces such as `ICommand`, `IQuery` and their respective handler-interfaces to cater to CQRS-like application-architecture, since thats what I am aiming for in a lot of my projects. I also added an `IRequestResponse` (and a generic variant) with a status code that mimics HTTP status codes, but using that interface is entirely optional (as is using the query and command APIs). When using the `ICommand` and `IQuery` markup-interfaces, the request responses are implicitly used by default without a way of opting out.

## Validation via attributes of request properties
The package `DandyMediator.Validation` adds automatic validation of properties (includes primary constructor parameters for records). If any property is deemed not valid, a response resembling _Bad Request_ and the occurred validation errors is returned. This allows automizing something as common as validation. However since an `IRequestResponse` is used as a return type, validation will not work when implementing requests without using an `IRequestResponse` as the requests return type.

Validation could look just like this:
```cs
internal sealed record RegisterUserCommand(
    [Required, MinLength(1), MaxLength(255)] string LoginName,
    [Required, MinLength(12)] string Password) : ICommand<Guid>;
```

## How do I use it?
I use DandyMediator in combination with my other package [DandyEndpoints](https://github.com/Nico1395/dandy-endpoints) for my interpretation of vertical slicing for every HTTP endpoint my APIs process. Have a look at this quick example:

```cs
internal static class GetTeamById
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/teams/{teamId}", async ([FromServices] IMediator mediator, Guid teamId, CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, Team>(new Query(teamId), cancellationToken);
                return response.ToResult();
            });
        }
    }

    private sealed record Query(Guid TeamId) : IQuery<Team>;

    private sealed class QueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<Query, Team>
    {
        public async Task<IQueryResponse<Team>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.Repository<ITeamRepository>().GetByIdAsync(request.TeamId, cancellationToken);
            return QueryResponseFactory.OkOrBadRequest(team).Build();
        }
    }
}
```

# Setup

You setup DandyMediator exactly like you would any other framework for an ASP.NET Core webapi. Have a quick look at this basic minimal setup:

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDandyMediator(configuration =>      // <- Just add this, just like you would expect
{
    configuration.ScanInAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.Run();
```

## Adding the validation plugin
Validation is configured using the `DandyMediatorConfigurationBuilder` when adding DandyMediator to the `WebApplicationBuilder`s `IServiceCollection`:
```cs
builder.Services.AddDandyMediator(configuration =>
{
    configuration.ScanInAssemblies(typeof(Program).Assembly);
    configuration.UseValidation();     // <- This line
});
```
