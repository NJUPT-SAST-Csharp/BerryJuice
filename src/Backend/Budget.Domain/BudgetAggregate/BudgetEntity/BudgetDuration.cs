namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public readonly record struct BudgetDuration(
    TimeSpan Duration,
    DateOnly BeginDate
)
{
    public override string ToString()
    {
        return $"BeginDate: {BeginDate}, Duration: {Duration}";
    }

    public (DateOnly PeriodBegin, DateOnly PeriodEnd) GetPeriod(DateOnly date)
    {
        var span = date.ToDateTime(new TimeOnly()) - BeginDate.ToDateTime(new TimeOnly());
        var cycles = (int)Math.Floor(span / Duration);

        return (BeginDate.AddDays(cycles * (int)Duration.TotalDays),
                BeginDate.AddDays((cycles + 1) * (int)Duration.TotalDays));
    }
}
