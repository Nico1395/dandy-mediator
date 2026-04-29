using Microsoft.Extensions.DependencyInjection;
using OpenMediator.Configuration;

namespace OpenMediator.Validation;

public static class MediatorConfigurationBuilderExtensions
{
    public static MediatorConfigurationBuilder UseValidation(this MediatorConfigurationBuilder builder, Action<ValidationMediatorPluginConfigurationBuilder>? configuration = null)
    {
        var plugin = new ValidationMediatorPlugin()
        {
            ConfigurationFactory = (services, mediatorConfiguration) =>
            {
                services.AddTransient(typeof(IRequestMiddleware<,>), typeof(ResponseRequestValidationMiddleware<,>));

                var configurationBuilder = new ValidationMediatorPluginConfigurationBuilder();
                configuration?.Invoke(configurationBuilder);

                return configurationBuilder.Build();
            },
        };

        return builder.UsePlugin(plugin);
    }
}
