namespace OpenMediator.Commands;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ICommandResponse>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TData> : IRequestHandler<TCommand, ICommandResponse<TData>>
    where TCommand : ICommand<TData>
{
}
