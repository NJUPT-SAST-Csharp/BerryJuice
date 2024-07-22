using Accounts.Domain.AccountAggregate.AccountEntity.Events;
using Accounts.Domain.AccountAggregate.AccountEntity.ValueObjects;
using Primitives.Entity;
using Shared.Primitives;

namespace Accounts.Domain.AccountAggregate.AccountEntity;

public class Account : EntityBase<AccountId>, IAggregateRoot<Account>
{
    private Account(DateTime date, decimal amount, MethodOfPayment methodOfPayment, string tag, string? description) :
        base(new AccountId(1))
    {
        _date = date;
        _amount = amount;
        _method = methodOfPayment;
        _tag = tag;
        _description = description;
    }

    public static Account CreateNewAccount(DateTime date, decimal amount, MethodOfPayment methodOfPayment, string tag,
        string? description)
    {
        var account = new Account(date, amount, methodOfPayment, tag, description);
        account.AddDomainEvent(new AccountCreatedDomainEvent(account.Id));
        return account;
    }

    private DateTime _date;

    private decimal _amount;

    private MethodOfPayment _method;
    private string _tag;

    private string? _description;
}