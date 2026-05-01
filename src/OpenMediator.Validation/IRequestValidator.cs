namespace OpenMediator.Validation;

public interface IRequestValidator
{
    IRequestResponseValidationResult? Validate(object request);
}
