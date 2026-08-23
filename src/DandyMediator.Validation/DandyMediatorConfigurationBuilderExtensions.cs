using DandyMediator.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DandyMediator.Validation;

public static class DandyMediatorConfigurationBuilderExtensions
{
    public static DandyMediatorConfigurationBuilder UseValidation(this DandyMediatorConfigurationBuilder builder, Action<DandyMediatorValidationPluginConfigurationBuilder>? configuration = null)
    {
        var configurationBuilder = new DandyMediatorValidationPluginConfigurationBuilder();
        configuration?.Invoke(configurationBuilder);
        var config = configurationBuilder.Build();

        var plugin = new DandyMediatorValidationPlugin
        {
            ConfigurationFactory = (services, _) =>
            {
                if (!config.Enabled)
                    return config;

                services.AddSingleton(config);
                services.AddTransient(typeof(IRequestMiddleware<,>), typeof(ResponseRequestValidationMiddleware<,>));
                services.AddSingleton<IRequestValidator, RequestValidator>();
                services.AddSingleton<IRequestValidationResponseFactory, RequestValidationResponseFactory>();

                return config;
            },
        };

        return builder.UsePlugin(plugin);
    }
}
