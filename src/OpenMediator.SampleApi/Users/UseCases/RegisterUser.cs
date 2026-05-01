using Microsoft.AspNetCore.Mvc;
using OpenMediator.Commands;
using OpenMediator.Responses;

namespace OpenMediator.SampleApi.Users.UseCases;

public static class RegisterUser
{
    public static void MapRegisterUser(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/register-user", async ([FromServices] IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand("fhdwadmin");
            var response = await mediator.SendAsync<RegisterUserCommand, Guid>(command, cancellationToken);

            return response.ToResult();
        });
    }

    private sealed record RegisterUserCommand(string LoginName) : ICommand<Guid>;

    private sealed class RegisterUserCommandHandler() : ICommandHandler<RegisterUserCommand, Guid>
    {
        public Task<ICommandResponse<Guid>> HandleAsync(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
