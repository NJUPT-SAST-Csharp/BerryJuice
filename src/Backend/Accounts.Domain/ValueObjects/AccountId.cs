namespace Accounts.Domain.ValueObjects;

public readonly record struct AccountId(long Value)
{
    public override string ToString() => Value.ToString();
}