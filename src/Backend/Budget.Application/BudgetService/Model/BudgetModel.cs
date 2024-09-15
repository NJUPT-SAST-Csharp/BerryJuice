namespace Budget.Application.BudgetService.Model;

public sealed record BudgetModel(
    long Id,
    long AccountId,
    decimal Amount,
    decimal Used,
    string Description,
    TimeSpan Duration,
    DateOnly BeginDate
);
