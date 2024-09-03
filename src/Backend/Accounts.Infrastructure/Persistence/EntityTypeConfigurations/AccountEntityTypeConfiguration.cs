using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.EntityTypeConfigurations;

internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(account => account.Id);
        builder
            .Property(account => account.Id)
            .HasColumnName("id")
            .HasConversion(x => x.Value, x => new AccountId(x));

        builder.Ignore(account => account.DomainEvents);

        builder
            .Property<AccountDescription>("_description")
            .HasColumnName("description")
            .HasConversion(x => x.Value, x => new AccountDescription(x));

        builder.HasMany<Transaction>(
            "_transactions"
        );
    }
}
