using Microsoft.Extensions.DependencyInjection;

namespace OpenMediator.Configuration;

public abstract class MediatorPlugin
{
    public abstract string Key { get; }
    public abstract string Slot { get; }
    public required Func<IServiceCollection, MediatorConfiguration, MediatorPluginConfiguration> ConfigurationFactory { get; init; }
}
