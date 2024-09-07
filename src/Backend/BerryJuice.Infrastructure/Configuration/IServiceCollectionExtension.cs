using System.Data.Common;
using System.Reflection;
using Accounts.Infrastructure.Configuration;
using Accounts.Infrastructure.Persistence;
using BerryJuice.Infrastructure.EventBus;
using BerryJuice.Infrastructure.Persistence;
using BerryJuice.Infrastructure.Persistence.QueryDatabase;
using BerryJuice.Infrastructure.Persistence.TypeConverters;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;
using Primitives;
using Primitives.Command;
using Primitives.DomainEvent;
using Primitives.EventBusScopedWrapper;
using Primitives.IntegrationEvent;
using Primitives.Query;
using Primitives.QueryDatabase;
using UnitOfWork = BerryJuice.Infrastructure.Persistence.UnitOfWork;

namespace BerryJuice.Infrastructure.Configuration;

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

    public static IServiceCollection ConfigureInternalEventBus(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            Accounts.Application.AssemblyReference.Assembly,
            Asset.Application.AssemblyReference.Assembly,
            Budget.Application.AssemblyReference.Assembly
        };

        services.AddScoped<IQueryRequestSender, InternalEventBus>();
        services.AddScoped<ICommandRequestSender, InternalEventBus>();
        services.AddScoped<IDomainEventPublisher, InternalEventBus>();

        services.AddSingleton<IEventBusWrapper, EventBusWrapper>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
        });
        return services;
    }

    public static IServiceCollection ConfigureExternalEventBus(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            Accounts.IntegrationEvent.AssemblyReference.Assembly,
            Asset.IntegrationEvent.AssemblyReference.Assembly,
            Budget.IntegrationEvent.AssemblyReference.Assembly
        };

        services.AddSingleton<IIntegrationEventPublisher, ExternalEventBus>();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
        });
        return services;
    }

    public static IServiceCollection ConfigureController(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        services.AddLogging();
        return services;
    }

    public static IServiceCollection ConfigureRepository(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection ConfigureLocalDatabase(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<BerryJuiceDbContext>(options =>
        {
            options
               .UseNpgsql(
                    connectionString,
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "bj_berryjuice")
                )
               .UseSnakeCaseNamingConvention();
        });

        services.AddDbContext<AccountsContext>(options =>
        {
            options
               .UseNpgsql(
                    connectionString,
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "bj_accounts")
                )
               .UseSnakeCaseNamingConvention();
        });

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>("berryjuice");

        SqlMapper.AddTypeHandler(new UriStringConverter());
        services.AddSingleton<DbDataSource>(new NpgsqlDataSourceBuilder(connectionString).Build());
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>(_ => new DbConnectionFactory(
                                                                          connectionString
                                                                      ));

        services.ConfigureAccountsRepository();
        // services.ConfigureAssetRepository();
        // services.ConfigureBudgetRepository();

        return services;
    }

    public static IServiceCollection ConfigureAzureDatabase(
        this IServiceCollection services,
        string connectionString
    )
    {
        return services;
    }
}