namespace Budget.Application.BudgetService.Model;

public sealed record BudgetModel(
    long Id,
    long AccountId,
    decimal Amount,
    string Description,
    TimeSpan Duration,
    DateOnly BeginDate
);
