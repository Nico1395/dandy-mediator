using OpenMediator.Responses;

namespace OpenMediator.Queries;

public sealed class QueryResponse<TData> : RequestResponse<TData>, IQueryResponse<TData>
{
    public QueryResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public QueryResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message, TData data)
        : base(status, metadata, message, data)
    {
    }
}
