using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.EntityTypeConfigurations;

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure (EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(transaction => transaction.Id);
        builder
           .Property(transaction => transaction.Id)
           .HasColumnName("id")
           .HasConversion(x => x.Value, x => new TransactionId(x));

        builder.Ignore(transaction => transaction.DomainEvents);

        builder.Property<DateTime>("_createdAt").HasColumnName("created_at");

        builder.ComplexProperty<TransactionAmount>(
            "_amount",
            builder =>
            {
                builder.Property(x => x.Amount).HasColumnName("amount");
                builder.Property(x => x.Currency).HasColumnName("currency");
            }
            );

        builder.Property<TransactionDescription>("_description")
               .HasColumnName("description").HasConversion(x => x.Value, x => new TransactionDescription(x));

        builder.HasMany<Tag>("_tags");
    }
}