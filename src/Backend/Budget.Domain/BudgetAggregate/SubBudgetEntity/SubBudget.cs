using Budget.Domain.BudgetAggregate.SubBudgetEntity.Events;
using Primitives.Entity;

namespace Budget.Domain.BudgetAggregate.SubBudgetEntity;

public class SubBudget : EntityBase<SubBudgetId>
{
    private SubBudget(string tag, decimal amount) : base(new SubBudgetId(1))
    {
        Tag = tag;
        Amount = amount;
    }
    public static SubBudget CreateSubBudget(string tag, decimal amount)
    {
        var subBudget = new SubBudget(tag, amount);
        subBudget.AddDomainEvent(new SubBudgetCreatedDomainEvent(subBudget.Id));
        return subBudget;
    }

    public string Tag { get; private set; }
    public decimal Amount { get; private set; }
}