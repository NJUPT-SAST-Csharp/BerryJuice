using Budget.Domain.BudgetAggregate;
using Budget.Infrastructure.DomainRepository;
using Budget.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Primitives;

namespace Budget.Infrastructure.Configuration;

public static class IServiceCollectionExtension
{
    public static IServiceCollection ConfigureBudgetRepository(this IServiceCollection services)
    {
        // Domain Repositories
        services.AddScoped<IBudgetRepository, BudgetRepository>();

        // Query Repositories

        // IUnitOfWork
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(serviceKey: "accounts");

        return services;
    }
}
