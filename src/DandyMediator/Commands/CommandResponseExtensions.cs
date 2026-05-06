using DandyMediator.Responses;

namespace DandyMediator.Commands;

public static class CommandResponseExtensions
{
    public static ICommandResponse<TDestination> Map<TSource, TDestination>(this ICommandResponse<TSource> response, Func<TSource, TDestination> map)
    {
        RequestResponseStatus status = response.Status;
        TDestination? data = default;

        // Only map the data if its code 200 and data is actually present
        if (response.IsSuccess_2xx() && response.Data != null)
            data = map(response.Data);

        return new CommandResponse<TDestination>(status)
        {
            Data = data,
            Metadata = response.Metadata,
        };
    }
}
