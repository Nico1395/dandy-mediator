using System.Reflection;
using DandyMediator.Tests.Fixtures;
using DandyMediator.Tests.Mocks;

namespace DandyMediator.Tests;

public class RequestSenderTests(DefaultFixture fixture) : IClassFixture<DefaultFixture>
{
    [Fact]
    public async Task RequestWithoutResponse_Handled()
    {
        var callback = new HandlerCallback();
        var request = new RequestWithoutResponse(callback);

        await fixture.GetMediator().SendAsync(request);

        Assert.Equal(1, callback.Successes);
    }

    [Fact]
    public async Task RequestWithoutResponseButNoHandler_Throws()
    {
        var request = new RequestWithoutResponseButNoHandler();

        await Assert.ThrowsAnyAsync<TargetInvocationException>(() => fixture.GetMediator().SendAsync(request));
    }

    [Fact]
    public async Task RequestWithResponseButNoHandler_Handled()
    {
        var request = new RequestWithResponse();
        var result = await fixture.GetMediator().SendAsync(request);

        Assert.True(result);
    }

    [Fact]
    public async Task RequestWithResponseButNoHandler_Throws()
    {
        var request = new RequestWithResponseButNoHandler();

        await Assert.ThrowsAnyAsync<Exception>(() => fixture.GetMediator().SendAsync(request));
    }
}