namespace OpenMediator.Responses;

public class RequestResponse : IRequestResponse
{
    public RequestResponse(RequestResponseStatus status)
    {
        Status = status;
    }

    public RequestResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message)
    {
        Status = status;
        Metadata = metadata ?? new Dictionary<string, object>();
        Message = message;
    }

    public RequestResponseStatus Status { get; }
    public IReadOnlyDictionary<string, object> Metadata { get; init; } = new Dictionary<string, object>();
    public string? Message { get; init; }
}

public class RequestResponse<TData> : RequestResponse, IRequestResponse<TData>
{
    public RequestResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public RequestResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message, TData? data)
        : base(status, metadata, message)
    {
        Data = data;
    }

    public TData? Data { get; init; }
}
