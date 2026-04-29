using OpenMediator.Responses;

namespace OpenMediator.Commands;

public interface ICommand : IResponseRequest<ICommandResponse>
{
}

public interface ICommand<TData> : IResponseRequest<ICommandResponse<TData>>
{
}
