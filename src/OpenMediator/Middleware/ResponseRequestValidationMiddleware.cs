using OpenMediator.Responses;

namespace OpenMediator.Middleware;

internal sealed class ResponseRequestValidationMiddleware<TRequest, TResponse> : ResponseRequestValidationMiddlewareBase, IRequestMiddleware<TRequest, TResponse>
    where TRequest : IResponseRequest<TResponse>
    where TResponse : IRequestResponse
{
    public async Task<TResponse> InterceptAsync(TRequest request, RequestHandlerDelegate<TResponse> nextStep, CancellationToken cancellationToken)
    {
        var result = ValidateRequest(request);
        if (result == null)
            return await nextStep.Invoke();

        return RequestValidationResponseFactory.BadRequest_400<TResponse>(result.Errors);
    }
}
