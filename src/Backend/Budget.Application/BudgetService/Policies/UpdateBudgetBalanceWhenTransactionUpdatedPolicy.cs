using Accounts.IntegrationEvent;
using Primitives.EventBusScopedWrapper;
using Primitives.IntegrationEvent;
using Primitives.Policy;

namespace Budget.Application.BudgetService.Policies;

public sealed class UpdateBudgetBalanceWhenTransactionUpdatedPolicy (IEventBusWrapper eventBus)
    : PolicyBase(eventBus),
        IIntegrationEventHandler<TransactionUpdatedIntegrationEvent>
{
    /// <inheritdoc />
    public async Task Handle (
        TransactionUpdatedIntegrationEvent notification,
        CancellationToken cancellationToken
    )
    {
        await
    }
}
