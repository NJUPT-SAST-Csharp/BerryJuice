using Primitives.DomainEvent;

namespace Accounts.Domain.AccountAggregate.AccountEntity.Events;

public class AccountDeletedDomainEvent(
    AccountId accountId
) : IDomainEvent
{
    public AccountId AccountId { get; } = accountId;
}
