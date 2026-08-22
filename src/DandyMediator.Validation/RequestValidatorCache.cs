using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DandyMediator.Validation;

internal static class RequestValidatorCache
{
    private static readonly ConcurrentDictionary<Type, RequestValidationMetadata> _cache = new();

    public static RequestValidationMetadata GetOrAdd(Type type)
    {
        return _cache.GetOrAdd(type, CreateMetadata);
    }

    private static RequestValidationMetadata CreateMetadata(Type type)
    {
        var validationProperties = type
            .GetProperties()
            .Where(p => p.GetCustomAttributes<ValidationAttribute>(true).Any())
            .ToList();

        return new RequestValidationMetadata(
            validationProperties.Count > 0,
            validationProperties
        );
    }
}
