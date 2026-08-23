using Microsoft.Extensions.DependencyInjection;

namespace DandyMediator;

internal sealed class RequestPipelineFactory(IServiceProvider serviceProvider) : IRequestPipelineFactory
{
    public RequestHandlerDelegate Create<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        var requestHandler = serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();
        var middlewares = serviceProvider.GetServices<IRequestMiddleware<TRequest>>();

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
        var requestHandler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var middlewares = serviceProvider.GetServices<IRequestMiddleware<TRequest, TResponse>>();
        
        RequestHandlerDelegate<TResponse> handlerDelegate = () => requestHandler.HandleAsync(request, cancellationToken);

        foreach (var middleware in middlewares.Reverse())
        {
            var next = handlerDelegate;
            handlerDelegate = () => middleware.InterceptAsync(request, next, cancellationToken);
        }

        return handlerDelegate;
    }
}
