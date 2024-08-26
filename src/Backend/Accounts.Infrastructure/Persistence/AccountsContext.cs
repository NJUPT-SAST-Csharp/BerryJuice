using Accounts.Domain.AccountAggregate.AccountEntity;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Persistence;

public class AccountsContext(DbContextOptions<AccountsContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BJAccounts");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsContext).Assembly);
    }
}
