using OpenMediator.Configuration;

namespace OpenMediator.Validation;

internal sealed class ValidationMediatorPlugin : MediatorPlugin
{
    public override string Key => MediatorConstants.Plugins.Validation.Key;
    public override string Slot => MediatorConstants.Plugins.Validation.Slot;
}
