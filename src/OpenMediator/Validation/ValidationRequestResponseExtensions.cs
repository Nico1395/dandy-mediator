using System.Diagnostics.CodeAnalysis;
using OpenMediator.Configuration;
using OpenMediator.Responses;

namespace OpenMediator.Validation;

public static class ValidationRequestResponseExtensions
{
    public static IRequestResponseValidationResult? GetValidationResult(this IRequestResponse response)
    {
        return response.GetMetadataValueOrDefault(MediatorConstants.Plugins.Validation.RequestMetadataKey) as IRequestResponseValidationResult;
    }

    public static bool TryGetValidationResult(this IRequestResponse response, [MaybeNullWhen(false)] out IRequestResponseValidationResult? result)
    {
        result = response.GetValidationResult();
        return result != null;
    }
}
