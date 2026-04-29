namespace OpenMediator.Validation;

public sealed class ValidationMediatorPluginConfigurationBuilder
{
    private readonly ValidationMediatorPluginConfiguration _configuration = new();

    public ValidationMediatorPluginConfigurationBuilder SetEnabled(bool enabled = true)
    {
        _configuration.Enabled = enabled;
        return this;
    }

    internal ValidationMediatorPluginConfiguration Build() => _configuration;
}
