namespace OpenMediator.Responses;

public interface IResponseRequest<TResponse> : IRequest<TResponse>
    where TResponse : IRequestResponse
{
}
