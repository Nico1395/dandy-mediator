using DandyMediator.Responses;

namespace DandyMediator.Validation;

internal sealed class ResponseRequestValidationMiddleware<TRequest, TResponse>(
    IRequestValidator requestValidator,
    IRequestValidationResponseFactory requestValidationResponseFactory) : IRequestMiddleware<TRequest, TResponse>
    where TRequest : IResponseRequest<TResponse>
    where TResponse : IRequestResponse
{
    public async Task<TResponse> InterceptAsync(TRequest request, RequestHandlerDelegate<TResponse> nextStep, CancellationToken cancellationToken)
    {
        var validationResult = requestValidator.Validate(request);
        if (validationResult != null)
            return requestValidationResponseFactory.CreateUnprocessableEntity<TResponse>(validationResult);

        return await nextStep.Invoke();
    }
}
