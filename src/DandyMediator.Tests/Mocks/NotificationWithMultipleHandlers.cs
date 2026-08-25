namespace DandyMediator.Tests.Mocks;

internal sealed record NotificationWithMultipleHandlers(HandlerCallback Callback) : INotification;

internal sealed class NotificationWithMultipleHandlersHandler1 : INotificationHandler<NotificationWithMultipleHandlers>
{
    public Task HandleAsync(NotificationWithMultipleHandlers notification, CancellationToken cancellationToken)
    {
        notification.Callback.Success();
        return Task.CompletedTask;
    }
}

internal sealed class NotificationWithMultipleHandlersHandler2 : INotificationHandler<NotificationWithMultipleHandlers>
{
    public Task HandleAsync(NotificationWithMultipleHandlers notification, CancellationToken cancellationToken)
    {
        notification.Callback.Success();
        return Task.CompletedTask;
    }
}