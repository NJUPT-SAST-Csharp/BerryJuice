using Accounts.Domain.AccountAggregate.AccountEntity.Events;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Primitives.Entity;
using Shared.Primitives;
using Utilities;

namespace Accounts.Domain.AccountAggregate.AccountEntity;

public class Account : EntityBase<AccountId>, IAggregateRoot<Account>
{
    private Account(AccountDescription description)
        : base(new AccountId(SnowFlakeIdGenerator.NewId))
    {
        _description = description;
    }

    public static Account CreateNewAccount(string description)
    {
        var account = new Account(new AccountDescription(description));
        account.AddDomainEvent(new AccountCreatedDomainEvent(account.Id));
        return account;
    }

    private readonly IList<Transaction> _transactions = [];

    private AccountDescription _description;
}
