namespace DandyMediator;

/// <summary>
/// Factory for resolving and composing a pipeline for a given mediator request.
/// </summary>
public interface IRequestPipelineFactory
{
    /// <summary>
    /// Creates a pipeline composed of a request handler and request middleware for the given <paramref name="request"/>.
    /// </summary>
    /// <typeparam name="TRequest">Type of request to compose a pipeline for.</typeparam>
    /// <param name="request">Request to compose a pipeline for.</param>
    /// <param name="cancellationToken">Cancellation token allowing the cancellation of asynchronous operations. Will be passed down to request handlers and middlewares.</param>
    /// <returns>A <see cref="RequestHandlerDelegate"/> representing the first middleware or handler to be invoked.</returns>
    RequestHandlerDelegate Create<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest;

    /// <summary>
    /// Creates a pipeline composed of a request handler and request middleware for the given <paramref name="request"/>.
    /// </summary>
    /// <typeparam name="TRequest">Type of request to compose a pipeline for.</typeparam>
    /// <typeparam name="TResponse">Type of response the <paramref name="request"/> returns.</typeparam>
    /// <param name="request">Request to compose a pipeline for.</param>
    /// <param name="cancellationToken">Cancellation token allowing the cancellation of asynchronous operations. Will be passed down to request handlers and middlewares.</param>
    /// <returns>A <see cref="RequestHandlerDelegate{TResponse}"/> representing the first middleware or handler to be invoked.</returns>
    RequestHandlerDelegate<TResponse> Create<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>;
}
