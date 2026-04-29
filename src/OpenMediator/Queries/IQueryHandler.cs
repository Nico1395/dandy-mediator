namespace OpenMediator.Queries;

public interface IQueryHandler<TQuery, TData> : IRequestHandler<TQuery, IQueryResponse<TData>>
    where TQuery : IQuery<TData>
{
}
