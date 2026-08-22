using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DandyMediator.Validation;

internal sealed class RequestValidator(
    DandyMediatorValidationPluginConfiguration configuration,
    IServiceProvider serviceProvider) : IRequestValidator
{
    public IRequestResponseValidationResult? Validate(object request)
    {
        var type = request.GetType();
        var metadata = RequestValidatorCache.GetOrAdd(type);
        if (!metadata.HasValidationAttributes)
            return null;

        var errors = new Dictionary<string, List<string>>(StringComparer.Ordinal);

        ValidateProperties(errors, request, metadata.ValidationProperties);

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

    private void ValidateProperties(Dictionary<string, List<string>> errors, object item, IReadOnlyList<PropertyInfo> validationProperties, string? parentPath = null, int depth = 0)
    {
        if (depth > configuration.RecursionDepth)
            return;

        // Providing the service provider, so custom validation attributes and use cases can reuse it.
        var context = new ValidationContext(item, serviceProvider, items: null);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(
            item,
            context,
            results,
            validateAllProperties: true);

        // When validating recursively, we need to prefix the member names with the parent path.
        var prefixedResults = results.Select(r =>
            new ValidationResult(
                r.ErrorMessage,
                r.MemberNames.Select(m => $"{parentPath}.{m}").ToArray()
            )
        );
        CollectErrors(prefixedResults, errors);

        foreach (var property in validationProperties)
        {
            // We re-use the metadata cache for the nested properties. This should be the most performant way of checking
            // whether a complex property even needs validation.
            var metadata = RequestValidatorCache.GetOrAdd(property.PropertyType);
            if (!metadata.HasValidationAttributes)
                continue;

            var value = property.GetValue(item);
            if (value == null)
                continue;

            ValidateProperties(
                errors,
                value,
                metadata.ValidationProperties,
                $"{parentPath}.{property.Name}",
                depth + 1);
        }
    }
}
