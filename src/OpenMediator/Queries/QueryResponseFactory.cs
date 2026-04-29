using OpenMediator.Responses;

namespace OpenMediator.Queries;

public static class QueryResponseFactory
{
    public static IQueryResponseBuilder<TData> OK_200<TData>(TData data) => new QueryResponseBuilder<TData>(RequestResponseStatus.OK_200, data);
    public static IQueryResponseBuilder<TData> Created_201<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.Created_201);
    public static IQueryResponseBuilder<TData> Accepted_202<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.Accepted_202);
    public static IQueryResponseBuilder<TData> NoContent_204<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.NoContent_204);
    public static IQueryResponseBuilder<TData> BadRequest_400<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.BadRequest_400);
    public static IQueryResponseBuilder<TData> Unauthorized_401<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.Unauthorized_401);
    public static IQueryResponseBuilder<TData> Forbidden_403<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.Forbidden_403);
    public static IQueryResponseBuilder<TData> NotFound_404<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.NotFound_404);
    public static IQueryResponseBuilder<TData> NotAcceptable_406<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.NotAcceptable_406);
    public static IQueryResponseBuilder<TData> Conflict_409<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.Conflict_409);
    public static IQueryResponseBuilder<TData> InternalServerError_500<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.InternalServerError_500);
    public static IQueryResponseBuilder<TData> NotImplemented_501<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.NotImplemented_501);
    public static IQueryResponseBuilder<TData> ServiceUnavailable_503<TData>() => new QueryResponseBuilder<TData>(RequestResponseStatus.ServiceUnavailable_503);

    public static IQueryResponseBuilder<TData> OkOrBadRequest<TData>(TData? data)
    {
        if (data == null)
            return BadRequest_400<TData>();

        return OK_200(data);
    }

    public static IQueryResponse<TData> FromData<TData>(TData? data) => (data == null ? BadRequest_400<TData>() : OK_200(data)).Build();
}
