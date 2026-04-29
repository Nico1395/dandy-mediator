using System.Reflection;

namespace OpenMediator.Configuration;

public sealed class MediatorConfiguration
{
    private readonly Dictionary<string, MediatorPlugin> _plugins = [];
    private readonly Dictionary<string, MediatorPluginConfiguration> _pluginConfigurations = [];
    private List<Assembly> _assemblies = [];

    public IReadOnlyDictionary<string, MediatorPlugin> Plugins => _plugins;
    public IReadOnlyList<Assembly> Assemblies => _assemblies;

    internal void AddPlugin(MediatorPlugin plugin)
    {
        _plugins[plugin.Slot] = plugin;
    }

    internal void AddPluginConfiguration(string slot, MediatorPluginConfiguration pluginConfiguration)
    {
        _pluginConfigurations[slot] = pluginConfiguration;
    }

    internal void SetAssemblies(IEnumerable<Assembly> assemblies)
    {
        _assemblies = assemblies.ToList();
    }
}
