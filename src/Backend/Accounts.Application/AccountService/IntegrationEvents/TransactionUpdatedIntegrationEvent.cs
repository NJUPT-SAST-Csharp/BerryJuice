using Primitives.IntegrationEvent;

namespace Accounts.Application.AccountService.IntegrationEvents;

public sealed record TransactionUpdatedIntegrationEvent(
    long TransactionId,
    long AccountId,
    DateTime TransactionTime,
    decimal OldAmount,
    decimal NewAmount
) : IIntegrationEvent { }
