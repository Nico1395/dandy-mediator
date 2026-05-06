using System.Reflection;

namespace DandyMediator.Configuration;

public sealed class DandyMediatorConfigurationBuilder
{
    private readonly DandyMediatorConfiguration _configuration = new();

    public DandyMediatorConfigurationBuilder ScanInAssemblies(params IEnumerable<Assembly> assemblies)
    {
        _configuration.SetAssemblies(assemblies);
        return this;
    }

    public DandyMediatorConfigurationBuilder UsePlugin(DandyMediatorPlugin plugin)
    {
        _configuration.AddPlugin(plugin);
        return this;
    }

    internal DandyMediatorConfiguration Build()
    {
        return _configuration;
    }
}
