using DandyMediator.Responses;

namespace DandyMediator.Queries;

public sealed class QueryResponse<TData> : RequestResponse<TData>, IQueryResponse<TData>
{
    public QueryResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public QueryResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, TData data)
        : base(status, metadata, data)
    {
    }
}
