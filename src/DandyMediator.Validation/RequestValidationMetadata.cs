using System.Reflection;

namespace DandyMediator.Validation;

internal sealed class RequestValidationMetadata(bool hasValidationAttributes, IReadOnlyList<PropertyInfo> validationProperties)
{
    public bool HasValidationAttributes { get; } = hasValidationAttributes;
    public IReadOnlyList<PropertyInfo> ValidationProperties { get; } = validationProperties;
}
