namespace Accounts.Domain.AccountAggregate.AccountEntity.ValueObjects;

public readonly record struct AccountId(long Value)
{
    public override string ToString() => Value.ToString();
}