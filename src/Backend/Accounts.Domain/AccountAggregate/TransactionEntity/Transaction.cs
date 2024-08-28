using Accounts.Domain.AccountAggregate.TransactionEntity.Event;
using Accounts.Domain.TagEntity;
using Primitives.Entity;
using Shared.Primitives;
using Utilities;

namespace Accounts.Domain.AccountAggregate.TransactionEntity;

public class Transaction : EntityBase<TransactionId>, IAggregateRoot<Transaction>
{
    private Transaction(
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

    // EF Core need this constructor due to limitations that it can't use constructor which has owned entity
    private Transaction(DateTime createdAt, TransactionDescription description)
        : base(new TransactionId(SnowFlakeIdGenerator.NewId))
    {
        _amount = new TransactionAmount();
        _createdAt = createdAt;
        _description = description;
        _tags = [];
    }

    public static Transaction CreateNewTransaction(
        TransactionAmount amount,
        DateTime createdAt,
        TransactionDescription description,
        List<Tag> tags
    )
    {
        var transaction = new Transaction(amount, createdAt, description, tags);
        transaction.AddDomainEvent(new TransactionCreatedDomainEvent(transaction.Id));
        return transaction;
    }

    private TransactionAmount _amount;

    private DateTime _createdAt;

    private TransactionDescription _description;

    private readonly List<Tag> _tags;
}
