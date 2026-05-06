using DandyMediator.Responses;

namespace DandyMediator.Queries;

public static class QueryResponseExtensions
{
    public static IQueryResponse<TDestination> Map<TSource, TDestination>(this IQueryResponse<TSource> response, Func<TSource, TDestination> map)
    {
        RequestResponseStatus status = response.Status;
        TDestination? data = default;

        // Only map the data if its code 200 and data is actually present
        if (response.IsOK_200() && response.Data != null)
            data = map(response.Data);
        else if (response.IsOK_200() && response.Data == null)
            status = RequestResponseStatus.BadRequest_400;

        return new QueryResponse<TDestination>(status)
        {
            Data = data,
            Metadata = response.Metadata,
        };
    }
}
