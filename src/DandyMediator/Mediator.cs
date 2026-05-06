using Microsoft.Extensions.DependencyInjection;

namespace DandyMediator;

internal sealed class Mediator(
    IRequestPipelineFactory _requestPipelineFactory,
    IServiceProvider _serviceProvider) : IMediator
{
    public Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var notificationHandlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();
        var handlerTasks = notificationHandlers.Select(notificationHandler => HandleNotificationAsync(notification, notificationHandler, cancellationToken));

        return Task.WhenAll(handlerTasks);
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        try
        {
            var handlerDelegate = _requestPipelineFactory.Create<TRequest, TResponse>(request, cancellationToken);
            return await handlerDelegate();
        }
        catch (Exception ex)
        {
            var exceptionHandler = _serviceProvider.GetService<IRequestExceptionHandler<TRequest, TResponse>>();
            if (exceptionHandler != null)
                await exceptionHandler.HandleAsync(request, ex, cancellationToken);

            throw;
        }
    }

    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        try
        {
            var handlerDelegate = _requestPipelineFactory.Create(request, cancellationToken);
            await handlerDelegate();
        }
        catch (Exception ex)
        {
            var exceptionHandler = _serviceProvider.GetService<IRequestExceptionHandler<TRequest>>();
            if (exceptionHandler != null)
                await exceptionHandler.HandleAsync(request, ex, cancellationToken);

            throw;
        }
    }

    private async Task HandleNotificationAsync<TNotification>(TNotification notification, INotificationHandler<TNotification> notificationHandler, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        try
        {
            await notificationHandler.HandleAsync(notification, cancellationToken);
        }
        catch (Exception ex)
        {
            var exceptionHandler = _serviceProvider.GetService<INotificationExceptionHandler<TNotification>>();
            if (exceptionHandler != null)
                await exceptionHandler.HandleAsync(notification, ex, cancellationToken);

            throw;
        }
    }
}
