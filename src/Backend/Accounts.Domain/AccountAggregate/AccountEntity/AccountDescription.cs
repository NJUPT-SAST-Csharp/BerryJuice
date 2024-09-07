namespace Accounts.Domain.AccountAggregate.AccountEntity;

public readonly record struct AccountDescription(string Value = "")
{
    public override string ToString() => Value;
}