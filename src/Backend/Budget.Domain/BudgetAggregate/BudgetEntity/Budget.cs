using Budget.Domain.BudgetAggregate.BudgetEntity.Events;
using Budget.Domain.BudgetAggregate.SubBudgetEntity;
using Budget.Domain.BudgetAggregate.TagEntity;
using Primitives.Entity;
using Shared.Primitives;

namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public class Budget : EntityBase<BudgetId>, IAggregateRoot<Budget>
{
    private Budget (DateTime startDate, DateTime endDate, decimal amount, string description)
        : base(new BudgetId(1))
    {
        _startDate = startDate;
        _endDate = endDate;
        _amount = amount;
        _description = description;
        SubBudgets = new List<SubBudget>();
    }

    public static Budget CreateBudget (
        DateTime startDate,
        DateTime endDate,
        decimal amount,
        string description
    )
    {
        var budget = new Budget(startDate, endDate, amount, description);
        budget.AddDomainEvent(new BudgetCreatedDomainEvent(budget.Id));
        return budget;
    }

    private DateTime _startDate;
    private DateTime _endDate;
    private decimal _amount;
    private string _description;
    private readonly List<SubBudget> SubBudgets;

    public void AddSubBudget (TagId tagId, decimal amount)
    {
        var subBudget = SubBudget.CreateSubBudget(tagId, amount);
        SubBudgets.Add(subBudget);
        subBudget.AddDomainEvent(new SubBudgetAddedDomainEvent(subBudget.Id));
    }
}
