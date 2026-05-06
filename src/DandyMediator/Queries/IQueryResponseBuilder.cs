namespace DandyMediator.Queries;

public interface IQueryResponseBuilder<TData>
{
    IQueryResponseBuilder<TData> WithMetadata(string key, object value);
    IQueryResponse<TData> Build();
}
