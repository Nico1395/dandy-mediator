using DandyMediator.Tests.Fixtures;
using DandyMediator.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace DandyMediator.Tests;

public class NotificationPublisherTests(DefaultFixture fixture) : IClassFixture<DefaultFixture>
{
    [Fact]
    public async Task NotificationWithOneHandler_HandledOnce()
    {
        var callback = new HandlerCallback();
        var notification = new NotificationWithOneHandler(callback);

        await fixture.GetRequiredService<IMediator>().PublishAsync(notification);

        Assert.Equal(1, callback.Successes);
    }

    [Fact]
    public async Task NotificationWithMultipleHandler_HandledTwice()
    {
        var callback = new HandlerCallback();
        var notification = new NotificationWithMultipleHandlers(callback);

        await fixture.GetRequiredService<IMediator>().PublishAsync(notification);

        Assert.Equal(2, callback.Successes);
    }

    [Fact]
    public async Task NotificationWithoutHandler_NotHandled()
    {
        var callback = new HandlerCallback();
        var notification = new NotificationWithoutHandler(callback);

        await fixture.GetRequiredService<IMediator>().PublishAsync(notification);

        Assert.Equal(0, callback.Successes);
    }
}