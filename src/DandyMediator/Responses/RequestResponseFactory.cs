using System.Collections.Concurrent;

namespace DandyMediator.Responses;

public class RequestResponseFactory : IRequestResponseFactory
{
    private static readonly ConcurrentDictionary<Type, bool> _typeIsResponseCache = [];

    public virtual object Create(Type responseType, params object?[] args)
    {
        if (!TypeIsResponse(responseType))
            throw new ArgumentException($"Type {responseType} is not a response type.");

        return Activator.CreateInstance(responseType, args) ?? throw new InvalidOperationException($"Failed to create instance of {responseType}.");
    }

    protected virtual bool TypeIsResponse(Type type)
    {
        return _typeIsResponseCache.GetOrAdd(type, t => t.IsAssignableTo(typeof(IRequestResponse)));
    }
}