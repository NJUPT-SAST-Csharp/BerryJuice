using System.Globalization;

namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public readonly record struct BudgetAmount(
    decimal Used, decimal Limit
)
{
    public override string ToString()
    {
        return $"{Used.ToString(CultureInfo.CurrentCulture)}/{Limit.ToString(CultureInfo.CurrentCulture)}";
    }
}
