using Accounts.Application.AccountService.Queries;
using Accounts.Application.TransactionService.Queries;
using Accounts.Domain.AccountAggregate;
using Accounts.Domain.TagEntity;
using Accounts.Infrastructure.DomainRepository;
using Accounts.Infrastructure.Persistence;
using Accounts.Infrastructure.QueryRepository;
using Microsoft.Extensions.DependencyInjection;
using Primitives;

namespace Accounts.Infrastructure.Configuration;

public static class IServiceCollectionExtension
{
    public static IServiceCollection ConfigureAccountsRepository(this IServiceCollection services)
    {
        // Domain Repositories
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        // Query Repositories
        services.AddScoped<IGetAccountsRepository, AccountQueryRepository>();
        services.AddScoped<IGetTransactionRepository, TransactionQueryRepository>();

        // IUnitOfWork
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(serviceKey: "accounts");

        return services;
    }
}
