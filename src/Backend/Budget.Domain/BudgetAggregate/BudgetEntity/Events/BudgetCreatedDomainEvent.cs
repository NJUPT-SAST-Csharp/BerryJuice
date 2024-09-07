using Primitives.DomainEvent;

namespace Budget.Domain.BudgetAggregate.BudgetEntity.Events;

public class BudgetCreatedDomainEvent(
    BudgetId budgetId
) : IDomainEvent
{
    public BudgetId BudgetId { get; } = budgetId;
}
