# Whats DandyMediator?
_DandyMediator_ is an opinionated implementation of the mediator pattern. It borrows a lot of its style from [JBogard's](https://github.com/jbogard) [MediatR](https://github.com/LuckyPennySoftware/MediatR), but eventually goes down another road. Its more focussed on enabling vertical slicing, CQRS and providing comfort features for both. The package should be setting itself apart from other options by now.

# Overview
## What can DandyMediator do?
DandyMediator should generally be able to do most things MediatR can do. There are a few core differences, such as the lack of streams. The configuration API also does not offer all that much other than the expected features. Thats mostly because I have not needed anything other than scanning for services in assemblies yet.

Additional markup-interfaces such as `ICommand`, `IQuery` and their respective handler-interfaces were added, to cater to CQRS-like application-architecture, since thats what this package aims for in a lot of my projects. I also added an `IRequestResponse` (and a generic variant) with a status code that mimics HTTP status codes, but using that interface is entirely optional (as is using the query and command APIs). When using the `ICommand` and `IQuery` markup-interfaces, the request responses are implicitly used by default without a way of opting out.

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

### Validation
The package `DandyMediator.Validation` adds automatic validation of properties or primary constructor parameters of records. If any property is deemed not valid, a response resembling a _404 Bad Request_ and the occurred validation errors is returned. This allows automizing something as common as validation. However since an `IRequestResponse` is used as a return type, validation will not work when implementing requests without using an `IRequestResponse` as the requests return type.

To validate a request, just place validation attributes on your properties or record parameters. Both should work.
```cs
internal sealed record RegisterUserCommand(
    [Required, MinLength(1), MaxLength(255)] string LoginName,
    [Required, MinLength(12)] string Password) : ICommand<IRequestResponse<Guid>>;
```
An appropriate response will be created (if using custom implementations of `IRequestResponse` or `IRequestResponse<TData>` are used, see below). That response will have a status of _422 Bad Request_.

### Custom request responses
If using custom implementations of `IRequestResponse` or `IRequestResponse<TData>`, you need to register an `IRequestResponseMap` so the **generic** abstract response type is associated with the **generic** implementation type. This map is used in the `IRequestResponseFactory` and allows implementing custom response types, if needed.

```cs
public interface ICustomRequestResponse : IRequestResponse;
public class CustomRequestResponse : RequestResponse, ICustomRequestResponse;

// Add this somewhere to your service collection
services.AddSingleton<IRequestResponseMap>(_ => new RequestResponseMap(typeof(ICustomRequestResponse), typeof(CustomRequestResponse)));
```

### Custom validation attributes
You can always implement custom validation attributes and use those as well. The `ValidationContext` of a validation attribute allows accessing an `IServiceProvider`, however its nullable and often times it is. DandyMediator provides the `IServiceProvider` of the service scope to the validator and thus custom validation attributes can access the service provider.

This approach aims to simplify validation and  make it more elegant. Adding yet another dependency just for basic validation use cases such as string lengths or so, is just not really useful, nor are those solutions as elegant as simple attributes added to a classes properties.

### Complex request properties
The framework allows for complex properties of requests. If a complex property type or a collection type is used as a request property, the `ValidateAttribute` has to be added like so:

```cs
// For complex properties
public sealed record ComplexProperty([StringLength(10)] string String);
public sealed record ComplexPropertyValidationRequest([Validate] ComplexProperty ComplexProperty) : IResponseRequest<IRequestResponse>;

// For enumerable properties
public sealed record EnumerableItem([StringLength(10)] string String);
public sealed record EnumerablePropertyValidationRequest([Validate] List<EnumerableItem> Items) : IResponseRequest<IRequestResponse>;
```

# Setup

You setup DandyMediator exactly like you would any other framework for an ASP.NET Core webapi. Have a quick look at this basic minimal setup:

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDandyMediator(configuration =>
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
    configuration.UseValidation();
});
```

# Response status conventions
Some extensions and factory methods provide quick facades for creating responses with certain response statuses. Even if this is a tribal subject amongst programmers, I settled on the following conventions:
- Success with content: 200
- Success without content: 204
- Content that was supposed to be queried or required during the request has not been found and the request returns: 404 (used by extension methods)
- A request is not valid: 422

If those conventions dont suit you, feel free to propose changes, but you can always create extensions of your own. The framework does not pack all HTTP codes but the ones that I assume will be sufficient for most use cases and you can work off of that as well.
