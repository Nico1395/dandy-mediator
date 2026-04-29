using System.Reflection;

namespace OpenMediator.Configuration;

public sealed class MediatorConfigurationBuilder
{
    private readonly MediatorConfiguration _configuration = new();

    public MediatorConfigurationBuilder ScanInAssemblies(params IEnumerable<Assembly> assemblies)
    {
        _configuration.SetAssemblies(assemblies);
        return this;
    }

    public MediatorConfigurationBuilder UsePlugin(MediatorPlugin plugin)
    {
        _configuration.AddPlugin(plugin);
        return this;
    }

    internal MediatorConfiguration Build()
    {
        return _configuration;
    }
}
