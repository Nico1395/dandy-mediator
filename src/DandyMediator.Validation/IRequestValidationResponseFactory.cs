using DandyMediator.Responses;

namespace DandyMediator.Validation;

public interface IRequestValidationResponseFactory
{
    TResponse CreateUnprocessableEntity<TResponse>(IRequestResponseValidationResult validationResult)
        where TResponse : IRequestResponse;
}
