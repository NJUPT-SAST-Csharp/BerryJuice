using System.Globalization;

namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public readonly record struct BudgetAmount(
    decimal Value
)
{
    public override string ToString()
    {
        return Value.ToString(CultureInfo.CurrentCulture);
    }
}
