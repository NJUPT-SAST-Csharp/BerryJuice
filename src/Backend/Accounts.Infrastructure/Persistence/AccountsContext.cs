using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.TagEntity;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Persistence;

public class AccountsContext(DbContextOptions<AccountsContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("bj_accounts");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountsContext).Assembly);
    }
}
