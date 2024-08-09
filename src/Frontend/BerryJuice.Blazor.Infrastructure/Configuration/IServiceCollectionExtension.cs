using BerryJuice.Blazor.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace BerryJuice.Blazor.Infrastructure.Configuration;

public static class IServiceCollectionExtension
{
    public static IServiceCollection ConfigureBlazor(this IServiceCollection services)
    {
        services
            .AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        services.AddSingleton<IEventBusWrapper, EventBusWrapper>();
        return services;
    }
}
