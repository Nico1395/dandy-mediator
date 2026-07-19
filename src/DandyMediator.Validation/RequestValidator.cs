using System.ComponentModel.DataAnnotations;

namespace DandyMediator.Validation;

internal sealed class RequestValidator : IRequestValidator
{
    public IRequestResponseValidationResult? Validate(object request)
    {
        var type = request.GetType();
        var metadata = RequestValidatorCache.GetOrAdd(type);
        if (!metadata.HasValidationAttributes)
            return null;

        var errors = new Dictionary<string, List<string>>(StringComparer.Ordinal);

        ValidateProperties(errors, request);

        if (errors.Count == 0)
            return null;

        return new RequestResponseValidationResult("Validation errors occurred", errors);
    }

    private static void CollectErrors(IEnumerable<ValidationResult> results, Dictionary<string, List<string>> errors)
    {
        foreach (var result in results)
        {
            foreach (var member in result.MemberNames)
            {
                if (!errors.TryGetValue(member, out var list))
                    errors[member] = list = [];

                list.Add(result.ErrorMessage ?? "Invalid value.");
            }
        }
    }

    private static void ValidateProperties(Dictionary<string, List<string>> errors, object request)
    {
        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(
            request,
            context,
            results,
            validateAllProperties: true);

        CollectErrors(results, errors);
    }
}
