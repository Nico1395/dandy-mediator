namespace DandyMediator.Commands;

/// <summary>
/// Contains extension methods for sending commands.
/// </summary>
public static class CommandRequestSenderExtensions
{
    /// <summary>
    /// Sends a command without response data.
    /// </summary>
    /// <typeparam name="TCommand">Type of command being sent.</typeparam>
    /// <param name="sender">Request sender.</param>
    /// <param name="command">Command being sent.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The command response.</returns>
    public static Task<ICommandResponse> SendAsync<TCommand>(this IRequestSender sender, TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        return sender.SendAsync<TCommand, ICommandResponse>(command, cancellationToken);
    }

    /// <summary>
    /// Sends a command that returns data.
    /// </summary>
    /// <typeparam name="TCommand">Type of command being sent.</typeparam>
    /// <typeparam name="TData">Type of response data.</typeparam>
    /// <param name="sender">Request sender.</param>
    /// <param name="command">Command being sent.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The command response.</returns>
    public static Task<ICommandResponse<TData>> SendAsync<TCommand, TData>(this IRequestSender sender, TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand<TData>
    {
        return sender.SendAsync<TCommand, ICommandResponse<TData>>(command, cancellationToken);
    }
}
