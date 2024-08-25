using Accounts.Domain.AccountAggregate.TagEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity.Event;
using Primitives.Entity;
using Shared.Primitives;
using Utilities;

namespace Accounts.Domain.AccountAggregate.TransactionEntity;

public class Transaction : EntityBase<TransactionId>, IAggregateRoot<Transaction>
{
    private Transaction(
        TransactionAmount amount,
        DateTime createdAt,
        TransactionDescription description
    )
        : base(new TransactionId(SnowFlakeIdGenerator.NewId))
    {
        _amount = amount;
        _createdAt = createdAt;
        _description = description;
    }

    public static Transaction CreateNewTransaction(
        TransactionAmount amount,
        DateTime createdAt,
        TagId[] tags,
        string description
    )
    {
        var transaction = new Transaction(
            amount,
            createdAt,
            new TransactionDescription(description)
        );
        transaction.AddDomainEvent(new TransactionCreatedDomainEvent(transaction.Id));
        return transaction;
    }

    private TransactionAmount _amount;

    private DateTime _createdAt;

    private TransactionDescription _description;

    private readonly TagId[] _tags = [];
}
