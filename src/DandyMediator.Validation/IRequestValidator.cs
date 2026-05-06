namespace DandyMediator.Validation;

public interface IRequestValidator
{
    IRequestResponseValidationResult? Validate(object request);
}
