namespace Accounts.Domain.AccountAggregate.TransactionEntity;

public readonly record struct TransactionId(long Value)
{
    public override string ToString() => Value.ToString();
}