using Budget.Application.BudgetService.Queries;
using Budget.Domain.BudgetAggregate;
using Budget.Infrastructure.DomainRepository;
using Budget.Infrastructure.Persistence;
using Budget.Infrastructure.QueryRepository;
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
        services.AddScoped<IGetBudgetRepository, BudgetQueryRepository>();

        // IUnitOfWork
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(serviceKey: "budget");

        return services;
    }
}
