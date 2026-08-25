namespace DandyMediator.Tests.Mocks;

internal sealed record RequestWithoutResponse(HandlerCallback Callback) : IRequest;

internal sealed class RequestWithoutResponseHandler : IRequestHandler<RequestWithoutResponse>
{
    public Task HandleAsync(RequestWithoutResponse request, CancellationToken cancellationToken)
    {
        request.Callback.Success();
        return Task.CompletedTask;
    }
}