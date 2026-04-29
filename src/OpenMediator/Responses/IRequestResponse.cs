namespace OpenMediator.Responses;

public interface IRequestResponse
{
    RequestResponseStatus Status { get; }
    IReadOnlyDictionary<string, object> Metadata { get; }
    string? Message { get; }
}

public interface IRequestResponse<TData> : IRequestResponse
{
    TData? Data { get; }
}
