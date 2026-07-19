namespace DandyMediator.Validation;

internal sealed class RequestValidationMetadata(bool hasValidationAttributes)
{
    public bool HasValidationAttributes { get; } = hasValidationAttributes;
}
