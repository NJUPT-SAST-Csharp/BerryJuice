namespace Budget.Domain.BudgetAggregate.TagEntity;

public readonly record struct TagId (long Value)
{
    public override string ToString ( ) => Value.ToString();
}
