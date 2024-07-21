using System.Reflection;
using BerryJuice.EventBus;
using Messenger;
using Microsoft.OpenApi.Models;
using Primitives.Command;
using Primitives.DomainEvent;
using Primitives.Query;

namespace BerryJuice.Configuration;

public static class IServiceCollectionExtension
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var scheme = new OpenApiSecurityScheme
            {
                Description = "Authorization Header \r\nExample:'Bearer 123456789'",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authorization"
                },
                Scheme = "oauth2",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            };
            options.AddSecurityDefinition("Authorization", scheme);
            var requirement = new OpenApiSecurityRequirement { [scheme] = [] };
            options.AddSecurityRequirement(requirement);
            var xmlFilename = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        return services;
    }

    public static IServiceCollection ConfigureBlazor(this IServiceCollection services)
    {
        services
            .AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        return services;
    }

    public static IServiceCollection ConfigureInternalEventBus(this IServiceCollection services)
    {
        services.AddScoped<IQueryRequestSender, InternalEventBus>();
        services.AddScoped<ICommandRequestSender, InternalEventBus>();
        services.AddScoped<IDomainEventPublisher, InternalEventBus>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Accounts.Application.AssemblyReference.Assembly);
            config.RegisterServicesFromAssembly(Asset.Application.AssemblyReference.Assembly);
            config.RegisterServicesFromAssembly(Budget.Application.AssemblyReference.Assembly);
        });
        return services;
    }

    public static IServiceCollection ConfigureExternalEventBus(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, ExternalEventBus>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(
                Accounts.IntegrationEvent.AssemblyReference.Assembly
            );
            config.RegisterServicesFromAssembly(Asset.IntegrationEvent.AssemblyReference.Assembly);
            config.RegisterServicesFromAssembly(Budget.IntegrationEvent.AssemblyReference.Assembly);
        });
        return services;
    }
}
