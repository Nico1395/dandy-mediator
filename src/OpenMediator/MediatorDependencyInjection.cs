using Microsoft.Extensions.DependencyInjection;
using OpenMediator.Commands;
using OpenMediator.Middleware;
using OpenMediator.Queries;
using System.Reflection;

namespace OpenMediator;

public static class MediatorDependencyInjection
{
    private static readonly IReadOnlyList<Type> _requestHandlerInterfaceTypes =
    [
        typeof(IRequestHandler<>),
        typeof(IRequestExceptionHandler<>),
        typeof(IRequestMiddleware<>),
        typeof(IRequestHandler<,>),
        typeof(IRequestExceptionHandler<,>),
        typeof(IRequestMiddleware<,>),
        typeof(IQueryHandler<,>),
        typeof(ICommandHandler<>),
        typeof(ICommandHandler<,>),
        typeof(INotificationHandler<>),
        typeof(INotificationExceptionHandler<>),
        
    ];

    public static IServiceCollection AddOpenMediator(this IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        services.AddTransient<IMediator, Mediator>();
        services.AddTransient(typeof(IRequestMiddleware<,>), typeof(ResponseRequestValidationMiddleware<,>));

        AddRequestHandlersFromAssemblies(services, assemblies);

        return services;
    }

    private static void AddRequestHandlersFromAssemblies(IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        var handlerTypes = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition);
        foreach (var implementationType in handlerTypes)
        {
            var interfaces = implementationType.ImplementedInterfaces;
            foreach (var @interface in interfaces)
            {
                if (!@interface.IsGenericType)
                    continue;

                var genericDefinition = @interface.GetGenericTypeDefinition();
                if (_requestHandlerInterfaceTypes.Contains(genericDefinition))
                    services.AddTransient(@interface, implementationType);
            }
        }
    }
}
