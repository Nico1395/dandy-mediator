using OpenMediator.Commands;
using OpenMediator.Queries;

namespace OpenMediator.Responses;

public static class RequestValidationResponseFactory
{
    public static TResponse BadRequest_400<TResponse>(IReadOnlyDictionary<string, string[]> errors)
        where TResponse : IRequestResponse
    {
        var metadata = new Dictionary<string, object>
        {
            ["title"] = "Validation failed",
            ["errors"] = errors
        };

        var responseType = typeof(TResponse);
        var implementationType = MapResponseInterfaceTypeToImplementationType(responseType);

        if (typeof(IRequestResponse).IsAssignableFrom(responseType) && responseType.IsGenericType)
        {
            return (TResponse)Activator.CreateInstance(
                implementationType,
                RequestResponseStatus.BadRequest_400,
                metadata,
                null,   // message
                null    // data
            )!;
        }

        return (TResponse)Activator.CreateInstance(
            implementationType,
            RequestResponseStatus.BadRequest_400,
            metadata,
            null    // message
        )!;
    }

    private static Type MapResponseInterfaceTypeToImplementationType(Type interfaceType)
    {
        // Non-generic
        if (interfaceType == typeof(IRequestResponse))
            return typeof(RequestResponse);

        if (interfaceType == typeof(ICommandResponse))
            return typeof(CommandResponse);

        // Generic interfaces
        if (interfaceType.IsGenericType)
        {
            var genericDef = interfaceType.GetGenericTypeDefinition();
            var genericArgs = interfaceType.GetGenericArguments();

            if (genericDef == typeof(IRequestResponse<>))
                return typeof(RequestResponse<>).MakeGenericType(genericArgs);

            if (genericDef == typeof(IQueryResponse<>))
                return typeof(QueryResponse<>).MakeGenericType(genericArgs);

            if (genericDef == typeof(ICommandResponse<>))
                return typeof(CommandResponse<>).MakeGenericType(genericArgs);
        }

        throw new NotSupportedException($"Response interface type '{interfaceType}' is not supported.");
    }
}
