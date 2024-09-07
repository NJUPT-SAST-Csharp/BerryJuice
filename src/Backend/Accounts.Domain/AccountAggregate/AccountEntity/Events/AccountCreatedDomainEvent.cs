using Shared.Primitives.DomainEvent;

namespace Accounts.Domain.AccountAggregate.AccountEntity.Events;

public class AccountCreatedDomainEvent(AccountId accountId) : IDomainEvent
{
    public AccountId AccountId { get; } = accountId;
}