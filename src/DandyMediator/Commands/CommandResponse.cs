using DandyMediator.Responses;

namespace DandyMediator.Commands;

public sealed class CommandResponse : RequestResponse, ICommandResponse
{
    public CommandResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public CommandResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata)
        : base(status, metadata)
    {
    }
}

public sealed class CommandResponse<TData> : RequestResponse<TData>, ICommandResponse<TData>
{
    public CommandResponse(RequestResponseStatus status)
        : base(status)
    {
    }

    public CommandResponse(RequestResponseStatus status, IReadOnlyDictionary<string, object>? metadata, TData data)
        : base(status, metadata, data)
    {
    }
}
