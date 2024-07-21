namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public readonly record struct BudgetId(long Value)
{
    public override string ToString()
    {
        return Value.ToString();
    }
}