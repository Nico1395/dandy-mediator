using System.Diagnostics.CodeAnalysis;
using DandyMediator.Configuration;
using DandyMediator.Responses;

namespace DandyMediator.Validation;

public static class ValidationRequestResponseExtensions
{
    public static IRequestResponseValidationResult? GetValidationResult(this IRequestResponse response)
    {
        return response.GetMetadataValueOrDefault(DandyMediatorConstants.Plugins.Validation.RequestMetadataKey) as IRequestResponseValidationResult;
    }

    public static bool TryGetValidationResult(this IRequestResponse response, [MaybeNullWhen(false)] out IRequestResponseValidationResult? result)
    {
        result = response.GetValidationResult();
        return result != null;
    }
}
