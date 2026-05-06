using DandyMediator.Responses;

namespace DandyMediator.Validation;

public interface IRequestValidationResponseFactory
{
    TResponse CreateBadRequest<TResponse>(IRequestResponseValidationResult validationResult)
        where TResponse : IRequestResponse;
}
