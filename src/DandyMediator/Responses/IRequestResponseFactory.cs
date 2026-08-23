namespace DandyMediator.Responses;

public interface IRequestResponseFactory
{
    object Create(Type responseType, params object?[] args);
}