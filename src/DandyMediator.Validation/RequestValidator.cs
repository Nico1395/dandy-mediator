using System.ComponentModel.DataAnnotations;
using System.Reflection;

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

        ValidateProperties(errors, request);                    // Validating properties for regular classes
        ValidateConstructors(errors, type, request);            // Validating constructors for records

        if (errors.Count == 0)
            return null;

        return new RequestResponseValidationResult("Validation errors occurred", errors.ToDictionary(k => k.Key, v => v.Value.ToList()));
    }

    private static void CollectErrors(IEnumerable<ValidationResult> results, Dictionary<string, List<string>> errors)
    {
        foreach (var result in results)
        {
            foreach (var member in result.MemberNames.Any() ? result.MemberNames : [string.Empty])
            {
                if (!errors.TryGetValue(member, out var list))
                {
                    list = [];
                    errors[member] = list;
                }

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

    private static void ValidateConstructors(Dictionary<string, List<string>> errors, Type requestType, object request)
    {
        foreach (var ctor in requestType.GetConstructors())
        {
            var parameters = ctor.GetParameters();
            if (parameters.Length == 0)
                continue;

            var values = parameters.Select(p => requestType.GetProperty(p.Name!)?.GetValue(request)).ToArray();
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                var value = values[i];

                var paramContext = new ValidationContext(request)
                {
                    MemberName = param.Name
                };

                var paramResults = new List<ValidationResult>();

                Validator.TryValidateValue(
                    value,
                    paramContext,
                    paramResults,
                    param.GetCustomAttributes<ValidationAttribute>(true));

                CollectErrors(paramResults, errors);
            }
        }
    }
}
