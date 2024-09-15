using Budget.Domain.BudgetAggregate.BudgetEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budget.Infrastructure.Persistence.EntityTypeConfiguration;

internal sealed class
    BudgetEntityTypeConfiguration : IEntityTypeConfiguration<Domain.BudgetAggregate.BudgetEntity.Budget>

{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.BudgetAggregate.BudgetEntity.Budget> builder)
    {
        builder.ToTable(name: "budgets");

        builder.HasKey(keyExpression: budget => budget.Id);
        builder
           .Property(propertyExpression: budget => budget.Id)
           .HasColumnName(name: "id")
           .HasConversion(
                convertToProviderExpression: x => x.Value,
                convertFromProviderExpression: x => new BudgetId(x)
            );

        builder
           .Property<AccountId>(propertyName: "_accountId")
           .HasColumnName(name: "account_id")
           .HasConversion(
                convertToProviderExpression: x => x.Value,
                convertFromProviderExpression: x => new AccountId(x)
            );

        builder.Ignore(propertyExpression: budget => budget.DomainEvents);

        builder
           .Property<BudgetDescription>(propertyName: "_description")
           .HasColumnName(name: "description")
           .HasConversion(
                convertToProviderExpression: x => x.Value,
                convertFromProviderExpression: x => new BudgetDescription(x)
            );

        builder.ComplexProperty<BudgetAmount>(
            propertyName: "_amount",
            buildAction: builder =>
            {
                builder.Property(x => x.Used).HasColumnName("used");
                builder.Property(x => x.Limit).HasColumnName("limit");
            }
        );

        builder.ComplexProperty<BudgetDuration>(
            propertyName: "_duration",
            buildAction: builder =>
            {
                builder.Property(x => x.Duration).HasColumnName("duration");
                builder.Property(x => x.BeginDate).HasColumnName("begin_date");
            }
        );
    }
}
