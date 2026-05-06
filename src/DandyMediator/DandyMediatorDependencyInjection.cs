using DandyMediator.Commands;
using DandyMediator.Configuration;
using DandyMediator.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DandyMediator;

public static class DandyMediatorDependencyInjection
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

    public static IServiceCollection AddDandyMediator(this IServiceCollection services, Action<DandyMediatorConfigurationBuilder>? configuration = null)
    {
        var builder = new DandyMediatorConfigurationBuilder();
        configuration?.Invoke(builder);
        var config = builder.Build();

        services.AddSingleton(config);
        services.AddTransient<IMediator, Mediator>();
        services.AddTransient<IRequestPipelineFactory, RequestPipelineFactory>();
        AddRequestHandlersFromAssemblies(services, config.Assemblies);

        InstallPlugins(services, config);       // Runs through plugins after the base services have been registered so a plugin could theoretically overwrite base registrations.

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

    private static void InstallPlugins(IServiceCollection services, DandyMediatorConfiguration configuration)
    {
        foreach (var plugin in configuration.Plugins.Values)
        {
            var pluginConfig = plugin.ConfigurationFactory(services, configuration);
            configuration.AddPluginConfiguration(plugin.Slot, pluginConfig);
        }
    }
}
