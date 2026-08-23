namespace DandyMediator.Responses;

public class RequestResponseMap(Type genericAbstractType, Type genericImplementationType) : IRequestResponseMap
{
    public Type GenericAbstractType => genericAbstractType;
    public Type GenericImplementationType => genericImplementationType;
}