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

        var plugin = new DandyMediatorValidationPlugin()
        {
            ConfigurationFactory = (services, mediatorConfiguration) =>
            {
                if (!config.Enabled)
                    return config;

                services.AddTransient(typeof(IRequestMiddleware<,>), typeof(ResponseRequestValidationMiddleware<,>));
                services.AddSingleton(typeof(IRequestValidator), typeof(RequestValidator));
                services.AddSingleton(typeof(IRequestValidationResponseFactory), typeof(RequestValidationResponseFactory));

                return config;
            },
        };

        return builder.UsePlugin(plugin);
    }
}
