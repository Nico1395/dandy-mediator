namespace DandyMediator.Validation;

public sealed class DandyMediatorValidationPluginConfigurationBuilder
{
    private readonly DandyMediatorValidationPluginConfiguration _configuration = new();

    public DandyMediatorValidationPluginConfigurationBuilder SetEnabled(bool enabled = true)
    {
        _configuration.Enabled = enabled;
        return this;
    }

    internal DandyMediatorValidationPluginConfiguration Build() => _configuration;
}
