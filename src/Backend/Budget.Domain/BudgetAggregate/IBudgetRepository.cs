using Budget.Domain.BudgetAggregate.BudgetEntity;

namespace Budget.Domain.BudgetAggregate;

public interface IBudgetRepository
{
    public Task<BudgetEntity.Budget> GetBudgetAsync(BudgetId id, CancellationToken cancellationToken = default);

    public Task<BudgetId> SaveBudgetAsync(BudgetEntity.Budget budget, CancellationToken cancellationToken = default);
}
