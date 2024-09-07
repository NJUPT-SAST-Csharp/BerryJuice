using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.EntityTypeConfigurations;

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(name: "transactions");

        builder.HasKey(keyExpression: transaction => transaction.Id);
        builder
           .Property(propertyExpression: transaction => transaction.Id)
           .HasColumnName(name: "id")
           .HasConversion(
                convertToProviderExpression: x => x.Value,
                convertFromProviderExpression: x => new TransactionId(x)
            );

        builder.Ignore(propertyExpression: transaction => transaction.DomainEvents);

        builder.Property<DateTime>(propertyName: "_createdAt").HasColumnName(name: "created_at");

        builder.ComplexProperty<TransactionAmount>(
            propertyName: "_amount",
            buildAction: builder =>
            {
                builder.Property(propertyExpression: x => x.Amount).HasColumnName(name: "amount");
                builder.Property(propertyExpression: x => x.Currency).HasColumnName(name: "currency");
            }
        );

        builder
           .Property<TransactionDescription>(propertyName: "_description")
           .HasColumnName(name: "description")
           .HasConversion(
                convertToProviderExpression: x => x.Value,
                convertFromProviderExpression: x => new TransactionDescription(x)
            );

        builder.HasMany<Tag>(navigationName: "_tags");
    }
}
