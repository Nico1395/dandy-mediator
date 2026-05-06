using DandyMediator.Responses;

namespace DandyMediator.Commands;

public interface ICommand : IResponseRequest<ICommandResponse>
{
}

public interface ICommand<TData> : IResponseRequest<ICommandResponse<TData>>
{
}
