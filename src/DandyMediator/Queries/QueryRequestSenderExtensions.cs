namespace DandyMediator.Queries;

/// <summary>
/// Contains extension methods for sending queries.
/// </summary>
public static class QueryRequestSenderExtensions
{
    /// <summary>
    /// Sends a query.
    /// </summary>
    /// <typeparam name="TQuery">Type of query being sent.</typeparam>
    /// <typeparam name="TData">Type of query result data.</typeparam>
    /// <param name="sender">Request sender.</param>
    /// <param name="query">Query being sent.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The query response.</returns>
    public static Task<IQueryResponse<TData>> SendAsync<TQuery, TData>(this IRequestSender sender, TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TData>
    {
        return sender.SendAsync<TQuery, IQueryResponse<TData>>(query, cancellationToken);
    }
}
