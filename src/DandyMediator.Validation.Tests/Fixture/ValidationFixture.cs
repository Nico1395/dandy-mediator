using Microsoft.Extensions.DependencyInjection;

namespace DandyMediator.Validation.Tests.Fixture;

public sealed class ValidationFixture : IServiceProvider
{
    private readonly IServiceProvider _serviceProvider;
    
    public ValidationFixture()
    {
        var services = new ServiceCollection();

        services.AddDandyMediator(config => config
            .ScanInAssemblies(typeof(ValidationFixture).Assembly)
            .UseValidation());

        _serviceProvider = services.BuildServiceProvider();
    }

    public object? GetService(Type serviceType)
    {
        return _serviceProvider.GetService(serviceType);
    }
}