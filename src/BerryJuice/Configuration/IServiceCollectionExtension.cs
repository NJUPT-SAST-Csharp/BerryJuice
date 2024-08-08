namespace BerryJuice.Configuration;

public static class IServiceCollectionExtension
{
    public static IServiceCollection ConfigureBlazor(this IServiceCollection services)
    {
        services
            .AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        return services;
    }
}
