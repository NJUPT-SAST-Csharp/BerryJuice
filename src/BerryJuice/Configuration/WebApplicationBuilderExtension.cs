using BerryJuice.Blazor.Infrastructure.Configuration;
using BerryJuice.Infrastructure.Configuration;
using Exceptions.Exceptions;

namespace BerryJuice.Configuration;

public static class WebApplicationBuilderExtension
{
    private static readonly Dictionary<string, string> switchMappings =
        new() { { "--use-blazor", "BERRYJUICE_USE_BLAZOR" } };

    public static WebApplicationBuilder ConfigureConfiguration(
        this WebApplicationBuilder builder,
        string[] args
    )
    {
        builder
           .Configuration.AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
           .AddEnvironmentVariables()
           .AddCommandLine(args, switchMappings)
           .Build();

        return builder;
    }

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var environment = builder.Environment;

        builder
           .Services.IfIsDevelopment(
                environment,
                services =>
                    services
                       .ConfigureSwagger()
                       .ConfigureLocalDatabase(
                            configuration.GetConnectionString("LocalPostgres")
                         ?? throw new ConnectionStringNotFoundException("LocalPostgres")
                        )
            )
           .IfIsNotDevelopment(
                environment,
                services =>
                    services.ConfigureAzureDatabase(
                        configuration.GetConnectionString("CSharpGroupAzure")
                     ?? throw new ConnectionStringNotFoundException("CSharpGroupAzure")
                    )
            )
           .IfBlazorEnabled(configuration, services => services.ConfigureBlazor())
           .ConfigureRepository()
           .ConfigureLogging()
           .ConfigureExternalEventBus()
           .ConfigureInternalEventBus()
           .ConfigureController();

        return builder;
    }
}