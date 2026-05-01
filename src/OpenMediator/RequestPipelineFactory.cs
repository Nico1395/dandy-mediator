using Microsoft.Extensions.DependencyInjection;

namespace OpenMediator;

internal sealed class RequestPipelineFactory(IServiceProvider _serviceProvider) : IRequestPipelineFactory
{
    public RequestHandlerDelegate Create<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        var requestHandler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();
        var middlewares = _serviceProvider.GetServices<IRequestMiddleware<TRequest>>();

        RequestHandlerDelegate handlerDelegate = () => requestHandler.HandleAsync(request, cancellationToken);

        foreach (var middleware in middlewares.Reverse())
        {
            var next = handlerDelegate;
            handlerDelegate = () => middleware.InterceptAsync(request, next, cancellationToken);
        }

        return handlerDelegate;
    }

    public RequestHandlerDelegate<TResponse> Create<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    {
        var requestHandler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var middlewares = _serviceProvider.GetServices<IRequestMiddleware<TRequest, TResponse>>();
        
        RequestHandlerDelegate<TResponse> handlerDelegate = () => requestHandler.HandleAsync(request, cancellationToken);

        foreach (var middleware in middlewares.Reverse())
        {
            var next = handlerDelegate;
            handlerDelegate = () => middleware.InterceptAsync(request, next, cancellationToken);
        }

        return handlerDelegate;
    }
}
