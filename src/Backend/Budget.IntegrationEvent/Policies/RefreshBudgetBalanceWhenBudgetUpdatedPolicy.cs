using Budget.Application.BudgetService.Commands;
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
        var command = new RefreshBudgetUsageCommand(notification.BudgetId.Value, 0);// TODO: Implement get used amount
        await _eventBus.CommandAsync(command, cancellationToken);
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
