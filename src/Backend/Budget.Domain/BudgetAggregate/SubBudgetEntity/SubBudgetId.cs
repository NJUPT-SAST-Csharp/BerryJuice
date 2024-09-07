namespace Budget.Domain.BudgetAggregate.SubBudgetEntity;

public readonly record struct SubBudgetId(
    long Value
)
{
    public override string ToString()
    {
        return Value.ToString();
    }
}
