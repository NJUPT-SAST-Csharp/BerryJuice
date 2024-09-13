using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budget.Infrastructure.Persistence.EntityTypeConfiguration;

internal sealed class BudgetEntityTypeConfiguration : IEntityTypeConfiguration<Domain.BudgetAggregate.BudgetEntity.Budget>

{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.BudgetAggregate.BudgetEntity.Budget> builder)
    {
        builder.ToTable(name: "budgets");
    }
}
