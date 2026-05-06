using DandyMediator.Responses;

namespace DandyMediator.Commands;

public static class CommandResponseFactory
{
    public static ICommandResponseBuilder OK_200() => new CommandResponseBuilder(RequestResponseStatus.OK_200);
    public static ICommandResponseBuilder Created_201() => new CommandResponseBuilder(RequestResponseStatus.Created_201);
    public static ICommandResponseBuilder Accepted_202() => new CommandResponseBuilder(RequestResponseStatus.Accepted_202);
    public static ICommandResponseBuilder NoContent_204() => new CommandResponseBuilder(RequestResponseStatus.NoContent_204);
    public static ICommandResponseBuilder BadRequest_400() => new CommandResponseBuilder(RequestResponseStatus.BadRequest_400);
    public static ICommandResponseBuilder Unauthorized_401() => new CommandResponseBuilder(RequestResponseStatus.Unauthorized_401);
    public static ICommandResponseBuilder Forbidden_403() => new CommandResponseBuilder(RequestResponseStatus.Forbidden_403);
    public static ICommandResponseBuilder NotFound_404() => new CommandResponseBuilder(RequestResponseStatus.NotFound_404);
    public static ICommandResponseBuilder NotAcceptable_406() => new CommandResponseBuilder(RequestResponseStatus.NotAcceptable_406);
    public static ICommandResponseBuilder Conflict_409() => new CommandResponseBuilder(RequestResponseStatus.Conflict_409);
    public static ICommandResponseBuilder InternalServerError_500() => new CommandResponseBuilder(RequestResponseStatus.InternalServerError_500);
    public static ICommandResponseBuilder NotImplemented_501() => new CommandResponseBuilder(RequestResponseStatus.NotImplemented_501);
    public static ICommandResponseBuilder ServiceUnavailable_503() => new CommandResponseBuilder(RequestResponseStatus.ServiceUnavailable_503);

    public static ICommandResponseBuilder<TData> OK_200<TData>(TData? data) => new CommandResponseBuilder<TData>(RequestResponseStatus.OK_200, data);
    public static ICommandResponseBuilder<TData> OK_200<TData>() => OK_200<TData>(data: default);
    public static ICommandResponseBuilder<TData> Created_201<TData>(TData? data) => new CommandResponseBuilder<TData>(RequestResponseStatus.Created_201, data);
    public static ICommandResponseBuilder<TData> Created_201<TData>() => Created_201<TData>(data: default);
    public static ICommandResponseBuilder<TData> Accepted_202<TData>(TData? data) => new CommandResponseBuilder<TData>(RequestResponseStatus.Accepted_202, data);
    public static ICommandResponseBuilder<TData> Accepted_202<TData>() => Accepted_202<TData>(data: default);
    public static ICommandResponseBuilder<TData> NoContent_204<TData>(TData? data) => new CommandResponseBuilder<TData>(RequestResponseStatus.NoContent_204, data);
    public static ICommandResponseBuilder<TData> NoContent_204<TData>() => NoContent_204<TData>(data: default);
    public static ICommandResponseBuilder<TData> BadRequest_400<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.BadRequest_400, data: default);
    public static ICommandResponseBuilder<TData> Unauthorized_401<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.Unauthorized_401, data: default);
    public static ICommandResponseBuilder<TData> Forbidden_403<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.Forbidden_403, data: default);
    public static ICommandResponseBuilder<TData> NotFound_404<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.NotFound_404, data: default);
    public static ICommandResponseBuilder<TData> NotAcceptable_406<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.NotAcceptable_406, data: default);
    public static ICommandResponseBuilder<TData> Conflict_409<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.Conflict_409, data: default);
    public static ICommandResponseBuilder<TData> InternalServerError_500<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.InternalServerError_500, data: default);
    public static ICommandResponseBuilder<TData> NotImplemented_501<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.NotImplemented_501, data: default);
    public static ICommandResponseBuilder<TData> ServiceUnavailable_503<TData>() => new CommandResponseBuilder<TData>(RequestResponseStatus.ServiceUnavailable_503, data: default);
}
