using Budget.Domain.BudgetAggregate.BudgetEntity.Events;
using Primitives.DomainEvent;
using Primitives.IntegrationEvent;

namespace Budget.Application.BudgetService.Policies;

public sealed class RefreshBudgetBalanceWhenBudgetUpdatedPolicy : IDomainEventHandler<BudgetCreatedDomainEvent>, IDomainEventHandler<BudgetUpdatedDomainEvent>
{
    /// <inheritdoc />
    public async Task Handle(BudgetCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task Handle(BudgetUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
