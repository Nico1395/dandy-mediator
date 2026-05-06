using DandyMediator.Commands;
using DandyMediator.Configuration;
using DandyMediator.Queries;
using DandyMediator.Responses;

namespace DandyMediator.Validation;

public class RequestValidationResponseFactory : IRequestValidationResponseFactory
{
    public TResponse CreateBadRequest<TResponse>(IRequestResponseValidationResult validationResult)
        where TResponse : IRequestResponse
    {
        var metadata = new Dictionary<string, object> { [DandyMediatorConstants.Plugins.Validation.RequestMetadataKey] = validationResult, };
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
            null        // message
        )!;
    }

    private static Type MapResponseInterfaceTypeToImplementationType(Type interfaceType)
    {
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
        else
        {
            if (interfaceType == typeof(IRequestResponse))
                return typeof(RequestResponse);

            if (interfaceType == typeof(ICommandResponse))
                return typeof(CommandResponse);
        }

        throw new NotSupportedException($"Response interface type '{interfaceType}' is not supported.");
    }
}
