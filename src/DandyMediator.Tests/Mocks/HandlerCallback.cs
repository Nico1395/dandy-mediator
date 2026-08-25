namespace DandyMediator.Tests.Mocks;

internal sealed class HandlerCallback
{
    public int Successes { get; private set; }

    public void Success()
    {
        Successes++;
    }
}