using Accounts.Domain.AccountAggregate.TagEntity;
using Budget.Domain.BudgetAggregate.BudgetEntity.Events;
using Budget.Domain.BudgetAggregate.SubBudgetEntity;
using Primitives.Entity;
using Shared.Primitives;
using Utilities;

namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public class Budget : EntityBase<BudgetId>, IAggregateRoot<Budget>
{
    private Budget(DateTime startDate, DateTime endDate, decimal amount, string description) : base(new BudgetId(SnowFlakeIdGenerator.NewId))
    {
        _startDate = startDate;
        _endDate = endDate;
        _amount = amount;
        _description = description;
        _subBudgets = new List<SubBudget>();
    }

    private DateTime _startDate;
    private DateTime _endDate;
    private decimal _amount;
    private string _description;
    private List<SubBudget> _subBudgets;


    public static Budget CreateBudget(DateTime startDate, DateTime endDate, decimal amount, string description)
    {
        var budget = new Budget(startDate, endDate, amount, description);
        budget.AddDomainEvent(new BudgetCreatedDomainEvent(budget.Id));
        return budget;
    }

    public void AddSubBudget(TagId tagId, decimal amount)
    {
        var subBudget = SubBudget.CreateSubBudget(tagId, amount);
        _subBudgets.Add(subBudget);
        subBudget.AddDomainEvent(new SubBudgetAddedDomainEvent(subBudget.Id));
    }
}