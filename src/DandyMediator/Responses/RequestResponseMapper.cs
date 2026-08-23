using DandyMediator.Commands;
using DandyMediator.Queries;

namespace DandyMediator.Responses;

public class RequestResponseMapper : IRequestResponseMapper
{
    public Type GetImplementationTypeFor(Type abstractResponseType)
    {
        if (abstractResponseType.IsGenericType)
        {
            var genericDef = abstractResponseType.GetGenericTypeDefinition();
            var genericArgs = abstractResponseType.GetGenericArguments();

            if (genericDef == typeof(IRequestResponse<>))
                return typeof(RequestResponse<>).MakeGenericType(genericArgs);

            if (genericDef == typeof(IQueryResponse<>))
                return typeof(QueryResponse<>).MakeGenericType(genericArgs);

            if (genericDef == typeof(ICommandResponse<>))
                return typeof(CommandResponse<>).MakeGenericType(genericArgs);
        }
        else
        {
            if (abstractResponseType == typeof(IRequestResponse))
                return typeof(RequestResponse);

            if (abstractResponseType == typeof(ICommandResponse))
                return typeof(CommandResponse);
        }

        throw new NotSupportedException($"Response interface type '{abstractResponseType}' is not supported.");
    }
}