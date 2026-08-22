using DandyMediator.Configuration;

namespace DandyMediator.Validation;

internal sealed class DandyMediatorValidationPluginConfiguration : DandyMediatorPluginConfiguration
{
    public bool Enabled { get; internal set; }
    public int RecursionDepth { get; internal set; } = 10;
}
