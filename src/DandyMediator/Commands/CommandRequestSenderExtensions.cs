namespace DandyMediator.Commands;

public static class CommandRequestSenderExtensions
{
    public static Task<ICommandResponse> SendAsync<TCommand>(this IRequestSender sender, TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        return sender.SendAsync<TCommand, ICommandResponse>(command, cancellationToken);
    }

    public static Task<ICommandResponse<TData>> SendAsync<TCommand, TData>(this IRequestSender sender, TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand<TData>
    {
        return sender.SendAsync<TCommand, ICommandResponse<TData>>(command, cancellationToken);
    }
}
