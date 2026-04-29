using OpenMediator.Responses;

namespace OpenMediator.Queries;

public interface IQuery<TData> : IResponseRequest<IQueryResponse<TData>>
{
}
