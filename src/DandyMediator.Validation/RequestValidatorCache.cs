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
        // Properties
        if (type.GetProperties().Any(p => p.GetCustomAttributes<ValidationAttribute>(true).Any()))
            return new RequestValidationMetadata(true);

        // Constructor parameter attributes for records
        var constructors = type.GetConstructors();
        foreach (var constructor in constructors)
        {
            if (constructor.GetParameters().Any(p => p.GetCustomAttributes<ValidationAttribute>(true).Any()))
                return new RequestValidationMetadata(true);
        }

        return new RequestValidationMetadata(false);
    }
}
