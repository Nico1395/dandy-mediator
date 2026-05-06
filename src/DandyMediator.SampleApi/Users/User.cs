namespace DandyMediator.SampleApi.Users;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string LoginName { get; init; }
}
