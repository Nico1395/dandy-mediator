namespace DandyMediator.Validation;

internal sealed class RequestValidationMetadata
{
    public RequestValidationMetadata()
    {
    }

    public RequestValidationMetadata(bool hasValidationAttributes)
    {
        HasValidationAttributes = hasValidationAttributes;
    }

    public bool HasValidationAttributes { get; init; }
}
