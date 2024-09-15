using Accounts.Application.AccountService.IntegrationEvents;
using Budget.Application.BudgetService.Commands;
using Primitives.EventBusScopedWrapper;
using Primitives.IntegrationEvent;
using Primitives.Policy;

namespace Budget.IntegrationEvent.Policies;

public sealed class UpdateBudgetBalanceWhenTransactionUpdatedPolicy(IEventBusWrapper eventBus)
    : PolicyBase(eventBus),
        IIntegrationEventHandler<TransactionUpdatedIntegrationEvent>
{
    /// <inheritdoc />
    public async Task Handle(
        TransactionUpdatedIntegrationEvent notification,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateBudgetUsageCommand(
            notification.AccountId,
            notification.OldAmount - notification.NewAmount
        );
        await _eventBus.CommandAsync(command, cancellationToken);
    }
}
