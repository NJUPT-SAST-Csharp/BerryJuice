namespace Accounts.Domain.AccountAggregate.TransactionEntity;

public readonly record struct TransactionDescription(string Value = "")
{
    public override string ToString() => Value;
}