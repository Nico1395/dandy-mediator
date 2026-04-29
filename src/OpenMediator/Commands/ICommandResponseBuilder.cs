namespace OpenMediator.Commands;

public interface ICommandResponseBuilder
{
    ICommandResponseBuilder WithMetadata(string key, object value);
    ICommandResponseBuilder WithMessage(string message);
    ICommandResponse Build();
}

public interface ICommandResponseBuilder<TData>
{
    ICommandResponseBuilder<TData> WithMetadata(string key, object value);
    ICommandResponseBuilder<TData> WithMessage(string message);
    ICommandResponse<TData> Build();
}
