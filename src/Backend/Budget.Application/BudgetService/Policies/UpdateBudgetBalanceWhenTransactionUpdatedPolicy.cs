using Accounts.IntegrationEvent;
using Primitives.IntegrationEvent;

namespace Budget.Application.BudgetService.Policies;

public sealed class UpdateBudgetBalanceWhenTransactionUpdatedPolicy(
    IServiceProvider serviceProvider
) : IIntegrationEventHandler<TransactionUpdatedIntegrationEvent>
{
    private IServiceProvider _serviceProvider = serviceProvider;


    /// <inheritdoc />
    public async Task Handle(TransactionUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
