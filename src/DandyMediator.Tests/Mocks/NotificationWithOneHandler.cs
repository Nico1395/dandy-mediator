namespace DandyMediator.Tests.Mocks;

internal sealed record NotificationWithOneHandler(HandlerCallback Callback) : INotification;

internal sealed class NotificationWithOneHandlerHandler : INotificationHandler<NotificationWithOneHandler>
{
    public Task HandleAsync(NotificationWithOneHandler notificationWithOneHandler, CancellationToken cancellationToken)
    {
        notificationWithOneHandler.Callback.Success();
        return Task.CompletedTask;
    }
}