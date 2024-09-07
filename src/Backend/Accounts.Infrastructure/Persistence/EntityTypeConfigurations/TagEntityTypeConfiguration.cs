using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Persistence.EntityTypeConfigurations;

internal class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable(name: "tags");

        builder.HasKey(keyExpression: tag => tag.Id);
        builder
           .Property(propertyExpression: tag => tag.Id)
           .HasColumnName(name: "id")
           .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new TagId(x));

        builder.Ignore(propertyExpression: tag => tag.DomainEvents);

        builder.Property<string>(propertyName: "_name").HasColumnName(name: "name").HasField(fieldName: "_name");

        builder
           .HasMany<Transaction>(navigationName: "_transactions")
           .WithMany(navigationName: "_tags")
           .UsingEntity(
                joinEntityName: "TransactionTag",
                configureRight: l => l.HasOne(typeof(Transaction)).WithMany().HasForeignKey("TransactionId"),
                configureLeft: r => r.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId"),
                configureJoinEntityType: j =>
                {
                    j.HasKey("TransactionId", "TagId");
                    j.ToTable(name: "transactions_tags");
                }
            );
    }
}
