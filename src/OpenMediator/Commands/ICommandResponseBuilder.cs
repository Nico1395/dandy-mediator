namespace OpenMediator.Commands;

public interface ICommandResponseBuilder
{
    ICommandResponseBuilder WithMetadata(string key, object value);
    ICommandResponse Build();
}

public interface ICommandResponseBuilder<TData>
{
    ICommandResponseBuilder<TData> WithMetadata(string key, object value);
    ICommandResponse<TData> Build();
}
