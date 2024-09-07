using Shared.Primitives.DomainEvent;

namespace Budget.Domain.BudgetAggregate.SubBudgetEntity.Events;

public class SubBudgetCreatedDomainEvent(
    SubBudgetId subBudgetId
) : IDomainEvent
{
    public SubBudgetId SubBudgetId { get; } = subBudgetId;
}
