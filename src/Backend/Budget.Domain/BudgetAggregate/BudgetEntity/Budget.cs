using Budget.Domain.BudgetAggregate.BudgetEntity.Events;
using Budget.Domain.BudgetAggregate.SubBudgetEntity;
using Primitives.Entity;
using Shared.Primitives;

namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public class Budget : EntityBase<BudgetId>, IAggregateRoot<Budget>
{
    private Budget(DateTime startDate, DateTime endDate, decimal amount, string description) : base(new BudgetId(1))
    {
        StartDate = startDate;
        EndDate = endDate;
        Amount = amount;
        Description = description;
        SubBudgets = new List<SubBudget>();
    }

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    private List<SubBudget> SubBudgets { get; set; }


    public static Budget CreateBudget(DateTime startDate, DateTime endDate, decimal amount, string description)
    {
        var budget = new Budget(startDate, endDate, amount, description);
        budget.AddDomainEvent(new BudgetCreatedDomainEvent(budget.Id));
        return budget;
    }

    public void AddSubBudget(string tag, decimal amount)
    {
        var subBudget = SubBudget.CreateSubBudget(tag, amount);
        SubBudgets.Add(subBudget);
        subBudget.AddDomainEvent(new SubBudgetAddedDomainEvent(subBudget.Id));
    }
}