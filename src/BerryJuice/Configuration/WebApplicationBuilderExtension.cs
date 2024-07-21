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
        }

        // builder.Services.ConfigureOptions(configuration);
        // SAST Image has this, but obviously it does nothing.

        builder.Services.AddLogging();

        if (IsBlazorEnabled(configuration))
            builder.Services.ConfigureBlazor();

        builder.Services.AddControllers();

        return builder;
    }

    private static readonly Dictionary<string, string> switchMappings =
        new() { { "--use-blazor", "BERRYJUICE_USE_BLAZOR" } };

    private static bool IsBlazorEnabled(ConfigurationManager configuration)
    {
        return configuration["BERRYJUICE_USE_BLAZOR"] == "true";
    }
}
