using DandyMediator.Configuration;

namespace DandyMediator.Validation;

internal sealed class DandyMediatorValidationPluginConfiguration : DandyMediatorPluginConfiguration
{
    public bool Enabled { get; internal set; }
}
