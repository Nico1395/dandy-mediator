using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OpenMediator.Middleware;

internal abstract class ResponseRequestValidationMiddlewareBase
{
    private sealed record ValidationMetadata(bool HasValidationAttributes);

    private static readonly ConcurrentDictionary<Type, ValidationMetadata> _cache = new();

    protected static ResponseRequestValidationResult? ValidateRequest(object request)
    {
        var type = request.GetType();
        var metadata = _cache.GetOrAdd(type, CreateMetadata);

        if (!metadata.HasValidationAttributes)
            return null;

        var errors = new Dictionary<string, List<string>>(StringComparer.Ordinal);

        ValidateProperties(errors, request);
        ValidateConstructors(errors, type, request);

        if (errors.Count == 0)
            return null;

        return new ResponseRequestValidationResult(errors.ToDictionary(k => k.Key, v => v.Value.ToArray()));
    }

    private static ValidationMetadata CreateMetadata(Type type)
    {
        // Properties
        if (type.GetProperties().Any(p => p.GetCustomAttributes<ValidationAttribute>(true).Any()))
            return new ValidationMetadata(true);

        // Constructor parameter attributes for records
        var constructors = type.GetConstructors();
        foreach (var constructor in constructors)
        {
            if (constructor.GetParameters().Any(p => p.GetCustomAttributes<ValidationAttribute>(true).Any()))
                return new ValidationMetadata(true);
        }

        return new ValidationMetadata(false);
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
