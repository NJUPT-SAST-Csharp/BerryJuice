using BerryJuice.Infrastructure.Configuration;
using Exceptions.Exceptions;

namespace BerryJuice.Configuration;

public static class WebApplicationBuilderExtension
{
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

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureLocalDatabase(
                configuration.GetConnectionString("LocalPostgres")
                    ?? throw new ConnectionStringNotFoundException("LocalPostgres")
            );
        }
        else
        {
            builder.Services.ConfigureLocalDatabase(
                configuration.GetConnectionString("CSharpGroupAzure")
                    ?? throw new ConnectionStringNotFoundException("CSharpGroupAzure")
            );
        }

        builder.Services.ConfigureRepository();

        builder.Services.AddLogging();

        if (IsBlazorEnabled(configuration))
            builder.Services.ConfigureBlazor();

        builder.Services.AddControllers();

        builder.Services.ConfigureExternalEventBus();
        builder.Services.ConfigureInternalEventBus();

        return builder;
    }

    private static readonly Dictionary<string, string> switchMappings =
        new() { { "--use-blazor", "BERRYJUICE_USE_BLAZOR" } };

    private static bool IsBlazorEnabled(ConfigurationManager configuration)
    {
        return configuration["BERRYJUICE_USE_BLAZOR"] == "true";
    }
}
