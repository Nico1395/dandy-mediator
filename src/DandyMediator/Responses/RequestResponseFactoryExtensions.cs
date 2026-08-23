namespace DandyMediator.Responses;

public static class RequestResponseFactoryExtensions
{
    public static TResponse CreateAndCast<TResponse>(this IRequestResponseFactory factory, Type responseType, params object?[] args)
        where TResponse : IRequestResponse
    {
        return (TResponse)factory.Create(responseType, args);
    }
}