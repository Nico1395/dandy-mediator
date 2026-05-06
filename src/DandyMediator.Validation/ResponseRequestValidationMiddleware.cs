using DandyMediator;
using DandyMediator.Responses;

namespace DandyMediator.Validation;

internal sealed class ResponseRequestValidationMiddleware<TRequest, TResponse>(
    IRequestValidator _requestValidator,
    IRequestValidationResponseFactory _requestValidationResponseFactory) : IRequestMiddleware<TRequest, TResponse>
    where TRequest : IResponseRequest<TResponse>
    where TResponse : IRequestResponse
{
    public async Task<TResponse> InterceptAsync(TRequest request, RequestHandlerDelegate<TResponse> nextStep, CancellationToken cancellationToken)
    {
        var validationResult = _requestValidator.Validate(request);
        if (validationResult != null)
            return _requestValidationResponseFactory.CreateBadRequest<TResponse>(validationResult);

        return await nextStep.Invoke();
    }
}
