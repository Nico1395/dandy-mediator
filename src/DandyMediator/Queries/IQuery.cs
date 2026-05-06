using DandyMediator.Responses;

namespace DandyMediator.Queries;

public interface IQuery<TData> : IResponseRequest<IQueryResponse<TData>>
{
}
