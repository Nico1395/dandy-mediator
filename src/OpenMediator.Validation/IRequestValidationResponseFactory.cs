using OpenMediator.Responses;

namespace OpenMediator.Validation;

public interface IRequestValidationResponseFactory
{
    TResponse CreateBadRequest<TResponse>(IRequestResponseValidationResult validationResult)
        where TResponse : IRequestResponse;
}
