namespace DandyMediator.Responses;

public interface IRequestResponseMapper
{
    Type GetImplementationTypeFor(Type abstractResponseType);
}