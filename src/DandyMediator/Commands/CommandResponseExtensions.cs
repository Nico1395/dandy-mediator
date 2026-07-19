using DandyMediator.Responses;

namespace DandyMediator.Commands;

public static class CommandResponseExtensions
{
    public static ICommandResponse<TDestination> Map<TSource, TDestination>(this ICommandResponse<TSource> response, Func<TSource, TDestination> map)
    {
        var status = response.Status;
        TDestination? data = default;

        // Only map the data if its code 200 and data is actually present
        if (response.IsOK_200() && response.Data != null)
            data = map(response.Data);
        else if (response.IsOK_200() && response.Data == null)
            status = RequestResponseStatus.NoContent_204;

        return new CommandResponse<TDestination>(status)
        {
            Data = data,
            Metadata = response.Metadata,
        };
    }
}
