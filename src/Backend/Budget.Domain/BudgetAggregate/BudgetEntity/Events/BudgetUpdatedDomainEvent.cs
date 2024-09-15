using Primitives.DomainEvent;

namespace Budget.Domain.BudgetAggregate.BudgetEntity.Events;

public sealed class BudgetUpdatedDomainEvent(BudgetId budgetId) : IDomainEvent
{
    public BudgetId BudgetId { get; } = budgetId;
}
