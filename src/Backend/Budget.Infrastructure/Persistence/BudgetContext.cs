using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Persistence;

public class BudgetContext(DbContextOptions<BudgetContext> options)
: DbContext(options)
{
    public DbSet<Domain.BudgetAggregate.BudgetEntity.Budget> Budgets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(schema: "bj_budget");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetContext).Assembly);

    }
}
