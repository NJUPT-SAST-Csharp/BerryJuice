using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.EntityTypeConfigurations;

internal class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");

        builder.HasKey(tag => tag.Id);
        builder
           .Property(tag => tag.Id)
           .HasColumnName("id")
           .HasConversion(x => x.Value, x => new TagId(x));

        builder.Ignore(tag => tag.DomainEvents);

        builder.Property<string>("_name").HasColumnName("name").HasField("_name");

        builder
           .HasMany<Transaction>("_transactions")
           .WithMany("_tags")
           .UsingEntity(
                "TransactionTag",
                l => l.HasOne(typeof(Transaction)).WithMany().HasForeignKey("TransactionId"),
                r => r.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId"),
                j =>
                {
                    j.HasKey("TransactionId", "TagId");
                    j.ToTable("transactions_tags");
                }
            );
    }
}