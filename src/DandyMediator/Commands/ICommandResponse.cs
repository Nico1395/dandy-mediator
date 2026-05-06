using DandyMediator.Responses;

namespace DandyMediator.Commands;

public interface ICommandResponse : IRequestResponse
{
}

public interface ICommandResponse<TData> : ICommandResponse, IRequestResponse<TData>
{
}
