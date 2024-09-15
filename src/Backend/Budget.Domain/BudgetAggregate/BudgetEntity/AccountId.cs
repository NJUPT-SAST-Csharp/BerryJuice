namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public readonly record struct AccountId(long Value)
{
    public override string ToString()
    {
        return Value.ToString();
    }
}
