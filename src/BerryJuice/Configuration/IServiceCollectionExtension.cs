namespace BerryJuice.Configuration;

internal static class IServiceCollectionExtension
{
    internal static IServiceCollection IfIsDevelopment(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        Action<IServiceCollection> action
    )
    {
        if (environment.IsDevelopment())
            action(services);
        return services;
    }

    internal static IServiceCollection IfIsNotDevelopment(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        Action<IServiceCollection> action
    )
    {
        if (!environment.IsDevelopment())
            action(services);
        return services;
    }

    internal static IServiceCollection IfBlazorEnabled(
        this IServiceCollection services,
        ConfigurationManager configuration,
        Action<IServiceCollection> action
    )
    {
        if (configuration["BERRYJUICE_USE_BLAZOR"] == "true")
            action(services);
        return services;
    }
}
