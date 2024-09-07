using Accounts.Domain.AccountAggregate.AccountEntity.Events;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity.Event;
using Accounts.Domain.TagEntity;
using Primitives.Entity;
using Utilities;

namespace Accounts.Domain.AccountAggregate.AccountEntity;

public class Account : EntityBase<AccountId>, IAggregateRoot<Account>
{
    private readonly List<Transaction> _transactions = [];

    private AccountDescription _description;

    private Account(AccountDescription description) : base(new AccountId(SnowFlakeIdGenerator.NewId))
    {
        _description = description;
    }

    public static Account CreateNewAccount(AccountDescription description)
    {
        var account = new Account(description);
        account.AddDomainEvent(new AccountCreatedDomainEvent(account.Id));
        return account;
    }

    public TransactionId AddTransaction(
        TransactionAmount amount,
        DateTime createdAt,
        TransactionDescription description,
        List<Tag> tags
    )
    {
        var transaction = Transaction.CreateNewTransaction(amount, createdAt, description, tags);

        _transactions.Add(transaction);

        transaction.AddDomainEvent(new TransactionCreatedDomainEvent(transaction.Id));

        return transaction.Id;
    }
}
