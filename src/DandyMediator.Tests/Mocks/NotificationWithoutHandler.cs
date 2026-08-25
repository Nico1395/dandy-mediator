namespace DandyMediator.Tests.Mocks;

internal sealed record NotificationWithoutHandler(HandlerCallback Callback) : INotification;
