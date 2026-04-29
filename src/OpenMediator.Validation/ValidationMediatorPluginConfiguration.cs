using OpenMediator.Configuration;

namespace OpenMediator.Validation;

internal sealed class ValidationMediatorPluginConfiguration : MediatorPluginConfiguration
{
    public bool Enabled { get; internal set; }
}
