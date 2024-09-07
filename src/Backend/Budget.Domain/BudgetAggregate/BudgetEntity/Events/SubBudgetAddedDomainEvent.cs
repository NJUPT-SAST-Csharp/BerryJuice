using Budget.Domain.BudgetAggregate.SubBudgetEntity;
using Primitives.DomainEvent;

namespace Budget.Domain.BudgetAggregate.BudgetEntity.Events;

public class SubBudgetAddedDomainEvent(
    SubBudgetId subBudgetId
) : IDomainEvent
{
    public SubBudgetId SubBudgetId { get; } = subBudgetId;
}
