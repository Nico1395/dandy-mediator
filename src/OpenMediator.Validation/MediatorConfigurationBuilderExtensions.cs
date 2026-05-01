using Microsoft.Extensions.DependencyInjection;
using OpenMediator.Configuration;

namespace OpenMediator.Validation;

public static class MediatorConfigurationBuilderExtensions
{
    public static MediatorConfigurationBuilder UseValidation(this MediatorConfigurationBuilder builder, Action<ValidationMediatorPluginConfigurationBuilder>? configuration = null)
    {
        var configurationBuilder = new ValidationMediatorPluginConfigurationBuilder();
        configuration?.Invoke(configurationBuilder);
        var config = configurationBuilder.Build();

        var plugin = new ValidationMediatorPlugin()
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
