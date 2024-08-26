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

        builder.OwnsMany<Transaction>(
            "_transactions",
            builder =>
            {
                builder.ToTable("transactions");
                builder.WithOwner().HasForeignKey("account_id");

                builder.HasKey(transaction => transaction.Id);
                builder
                    .Property(transaction => transaction.Id)
                    .HasColumnName("id")
                    .HasConversion(x => x.Value, x => new TransactionId(x));

                builder.Ignore(transaction => transaction.DomainEvents);

                builder.Property<DateTime>("_createdAt").HasColumnName("created_at");

                builder.OwnsOne<TransactionAmount>(
                    "_amount",
                    builder =>
                    {
                        builder.Property(x => x.Amount).HasColumnName("amount");
                        builder.Property(x => x.Currency).HasColumnName("currency");
                    }
                );
            }
        );
    }
}
