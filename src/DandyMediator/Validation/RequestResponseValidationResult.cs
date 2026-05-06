namespace DandyMediator.Validation;

public class RequestResponseValidationResult : IRequestResponseValidationResult
{
    public RequestResponseValidationResult(string title, Dictionary<string, List<string>> errors)
    {
        Title = title;
        Errors = errors.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToList() as IReadOnlyList<string>);
    }

    public string Title { get; }
    public IReadOnlyDictionary<string, IReadOnlyList<string>> Errors { get; }
}
