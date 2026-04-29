using OpenMediator.Responses;

namespace OpenMediator.Commands;

public interface ICommandResponse : IRequestResponse
{
}

public interface ICommandResponse<TData> : ICommandResponse, IRequestResponse<TData>
{
}
