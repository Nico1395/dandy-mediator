namespace OpenMediator;

/// <summary>
/// Global exception handler for all <see cref="IRequest"/>/<see cref="IRequest{TResponse}"/>.
/// </summary>
public interface IGlobalRequestExceptionHandler
{
    /// <summary>
    /// Handles the given <paramref name="exception"/> thrown in a handler or in middleware for the <paramref name="request"/>.
    /// </summary>
    /// <param name="request">Request the exception has been thrown for.</param>
    /// <param name="exception">Thrown exception.</param>
    /// <param name="cancellationToken">Cancellation token for aborting asynchronous operations.</param>
    Task HandleAsync(object request, Exception exception, CancellationToken cancellationToken);
}
