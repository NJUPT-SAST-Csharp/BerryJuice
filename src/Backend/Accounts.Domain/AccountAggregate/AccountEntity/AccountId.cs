namespace Accounts.Domain.AccountAggregate.AccountEntity;

public readonly record struct AccountId(
    long Value
)
{
    public override string ToString()
    {
        return Value.ToString();
    }
}
