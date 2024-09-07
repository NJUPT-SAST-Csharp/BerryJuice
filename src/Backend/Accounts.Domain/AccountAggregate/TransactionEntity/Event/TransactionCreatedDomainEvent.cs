using Shared.Primitives.DomainEvent;

namespace Accounts.Domain.AccountAggregate.TransactionEntity.Event;

public class TransactionCreatedDomainEvent(TransactionId id) : IDomainEvent
{
    public TransactionId Id { get; } = id;
}