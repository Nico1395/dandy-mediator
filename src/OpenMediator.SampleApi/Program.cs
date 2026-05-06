using OpenMediator.SampleApi.Users.UseCases;
using OpenMediator.Validation;

namespace OpenMediator.SampleApi;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddOpenMediator(configuration =>
        {
            configuration.ScanInAssemblies(typeof(Program).Assembly);
            configuration.UseValidation();
        });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
            app.MapOpenApi();

        app.UseHttpsRedirection();

        app.MapRegisterUser();

        app.Run();
    }
}
