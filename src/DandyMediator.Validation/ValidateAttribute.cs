using System.ComponentModel.DataAnnotations;

namespace DandyMediator.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class ValidateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return true;
    }
}