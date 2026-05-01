namespace OpenMediator;

public interface IRequestPipelineFactory
{
    RequestHandlerDelegate Create<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest;
    RequestHandlerDelegate<TResponse> Create<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>;
}
