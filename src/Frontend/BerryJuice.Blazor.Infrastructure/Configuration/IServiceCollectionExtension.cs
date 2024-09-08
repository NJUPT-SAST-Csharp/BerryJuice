using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace BerryJuice.Blazor.Infrastructure.Configuration;

public static class IServiceCollectionExtension
{
    public static IServiceCollection ConfigureBlazor(this IServiceCollection services)
    {
        services.AddMudServices();

        services
            .AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        return services;
    }
}
