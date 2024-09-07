using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.EntityTypeConfigurations;

internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(name: "accounts");

        builder.HasKey(keyExpression: account => account.Id);
        builder
           .Property(propertyExpression: account => account.Id)
           .HasColumnName(name: "id")
           .HasConversion(
                convertToProviderExpression: x => x.Value,
                convertFromProviderExpression: x => new AccountId(x)
            );

        builder.Ignore(propertyExpression: account => account.DomainEvents);

        builder
           .Property<AccountDescription>(propertyName: "_description")
           .HasColumnName(name: "description")
           .HasConversion(
                convertToProviderExpression: x => x.Value,
                convertFromProviderExpression: x => new AccountDescription(x)
            );

        builder.HasMany<Transaction>(navigationName: "_transactions");
    }
}
