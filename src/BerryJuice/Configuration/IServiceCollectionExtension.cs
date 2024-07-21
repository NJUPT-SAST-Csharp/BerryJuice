using System.Reflection;
using Microsoft.OpenApi.Models;

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
}
