using Primitives.DomainEvent;

namespace Budget.Domain.BudgetAggregate.BudgetEntity.Events;

public sealed class BudgetCreatedDomainEvent(
    BudgetId budgetId
) : IDomainEvent
{
    public BudgetId BudgetId { get; } = budgetId;
}
