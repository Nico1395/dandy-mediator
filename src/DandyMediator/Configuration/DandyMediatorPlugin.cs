using Microsoft.Extensions.DependencyInjection;

namespace DandyMediator.Configuration;

public abstract class DandyMediatorPlugin
{
    public abstract string Key { get; }
    public abstract string Slot { get; }
    public required Func<IServiceCollection, DandyMediatorConfiguration, DandyMediatorPluginConfiguration> ConfigurationFactory { get; init; }
}
