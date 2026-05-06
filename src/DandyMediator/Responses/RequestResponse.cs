namespace DandyMediator.Responses;

public class RequestResponse : IRequestResponse
{
    public RequestResponse(RequestResponseStatus status)
    {
        Status = status;
    }

    public RequestResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata)
    {
        Status = status;
        Metadata = metadata ?? new Dictionary<string, object>();
    }

    public RequestResponseStatus Status { get; }
    public IReadOnlyDictionary<string, object> Metadata { get; init; } = new Dictionary<string, object>();
}

public class RequestResponse<TData> : RequestResponse, IRequestResponse<TData>
{
    public RequestResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public RequestResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, TData? data)
        : base(status, metadata)
    {
        Data = data;
    }

    public TData? Data { get; init; }
}
