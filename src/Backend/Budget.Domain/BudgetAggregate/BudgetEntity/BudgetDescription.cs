namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public readonly record struct BudgetDescription(
    string Value
)
{
    public override string ToString()
    {
        return Value;
    }
}
