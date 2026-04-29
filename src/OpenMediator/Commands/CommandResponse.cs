using OpenMediator.Responses;

namespace OpenMediator.Commands;

public sealed class CommandResponse : RequestResponse, ICommandResponse
{
    public CommandResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public CommandResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message)
        : base(status, metadata, message)
    {
    }
}

public sealed class CommandResponse<TData> : RequestResponse<TData>, ICommandResponse<TData>
{
    public CommandResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public CommandResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message, TData data)
        : base(status, metadata, message, data)
    {
    }
}
