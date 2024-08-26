using Accounts.Domain.AccountAggregate.TransactionEntity.Event;
using Accounts.Domain.TagEntity;
using Primitives.Entity;
using Shared.Primitives;
using Utilities;

namespace Accounts.Domain.AccountAggregate.TransactionEntity;

public class Transaction : EntityBase<TransactionId>, IAggregateRoot<Transaction>
{
    private Transaction (
        TransactionAmount amount,
        DateTime createdAt,
        TransactionDescription description,
        List<Tag> tags
    )
        : base(new TransactionId(SnowFlakeIdGenerator.NewId))
    {
        _amount = amount;
        _createdAt = createdAt;
        _description = description;
        _tags = tags;
    }

    private Transaction (
        DateTime createdAt,
        TransactionDescription description
    ) // EF Core need this
        : base(new TransactionId(SnowFlakeIdGenerator.NewId))
    {
        _amount = new TransactionAmount();
        _createdAt = createdAt;
        _description = description;
        _tags = [ ];
    }

    public static Transaction CreateNewTransaction (
        TransactionAmount amount,
        DateTime createdAt,
        string description,
        List<Tag> tags
    )
    {
        var transaction = new Transaction(
            amount,
            createdAt,
            new TransactionDescription(description),
            tags
        );
        transaction.AddDomainEvent(new TransactionCreatedDomainEvent(transaction.Id));
        return transaction;
    }

    private TransactionAmount _amount;

    private DateTime _createdAt;

    private TransactionDescription _description;

    private readonly List<Tag> _tags;
}
