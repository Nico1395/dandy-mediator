using OpenMediator.Responses;

namespace OpenMediator.Commands;

internal sealed class CommandResponseBuilder(RequestResponseStatus status) : ICommandResponseBuilder
{
    private string? _message;
    private Dictionary<string, object> _metadata = [];

    public ICommandResponseBuilder WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public ICommandResponseBuilder WithMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }

    public ICommandResponse Build()
    {
        return new CommandResponse(status)
        {
            Message = _message,
            Metadata = _metadata
        };
    }
}

internal sealed class CommandResponseBuilder<TData>(RequestResponseStatus status, TData? data) : ICommandResponseBuilder<TData>
{
    private string? _message;
    private Dictionary<string, object> _metadata = [];

    public ICommandResponseBuilder<TData> WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public ICommandResponseBuilder<TData> WithMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }

    public ICommandResponse<TData> Build()
    {
        return new CommandResponse<TData>(status)
        {
            Data = data,
            Message = _message,
            Metadata = _metadata
        };
    }
}
