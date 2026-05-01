namespace OpenMediator.Validation;

public interface IRequestResponseValidationResult
{
    string Title { get; }
    IReadOnlyDictionary<string, IReadOnlyList<string>> Errors { get; }
}
