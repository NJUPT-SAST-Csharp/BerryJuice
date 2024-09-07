using Shared.Primitives.DomainEvent;

namespace Accounts.Domain.AccountAggregate.AccountEntity.Events;

public class AccountUpdatedDomainEvent(
    AccountId accountId
) : IDomainEvent
{
    public AccountId AccountId { get; } = accountId;
}
