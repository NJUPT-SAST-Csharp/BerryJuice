using Budget.Domain.BudgetAggregate.BudgetEntity.Events;
using Primitives.DomainEvent;
using Primitives.EventBusScopedWrapper;
using Primitives.Policy;

namespace Budget.IntegrationEvent.Policies;

public sealed class RefreshBudgetBalanceWhenBudgetUpdatedPolicy(IEventBusWrapper eventBus)
    : PolicyBase(eventBus),
        IDomainEventHandler<BudgetCreatedDomainEvent>,
        IDomainEventHandler<BudgetUpdatedDomainEvent>
{
    /// <inheritdoc />
    public async Task Handle(
        BudgetCreatedDomainEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _eventBus.QueryAsync();
    }

    /// <inheritdoc />
    public async Task Handle(
        BudgetUpdatedDomainEvent notification,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
