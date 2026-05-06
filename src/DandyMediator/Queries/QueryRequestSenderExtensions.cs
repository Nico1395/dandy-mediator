namespace DandyMediator.Queries;

public static class QueryRequestSenderExtensions
{
    public static Task<IQueryResponse<TData>> SendAsync<TQuery, TData>(this IRequestSender sender, TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TData>
    {
        return sender.SendAsync<TQuery, IQueryResponse<TData>>(query, cancellationToken);
    }
}
