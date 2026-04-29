namespace OpenMediator.Middleware;

internal sealed record ResponseRequestValidationResult(IReadOnlyDictionary<string, string[]> Errors)
{
    public bool IsValid => Errors.Count == 0;
}
