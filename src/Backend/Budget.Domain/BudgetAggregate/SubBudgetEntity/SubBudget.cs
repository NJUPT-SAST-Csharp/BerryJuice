using Budget.Domain.BudgetAggregate.SubBudgetEntity.Events;
using Budget.Domain.BudgetAggregate.TagEntity;
using Primitives.Entity;
using Utilities;

namespace Budget.Domain.BudgetAggregate.SubBudgetEntity;

public class SubBudget : EntityBase<SubBudgetId>
{
    private SubBudget(TagId tagId, decimal amount)
        : base(new SubBudgetId(SnowFlakeIdGenerator.NewId))
    {
        _tagId = tagId;
        _amount = amount;
    }

    public static SubBudget CreateSubBudget(TagId tagId, decimal amount)
    {
        var subBudget = new SubBudget(tagId, amount);
        subBudget.AddDomainEvent(new SubBudgetCreatedDomainEvent(subBudget.Id));
        return subBudget;
    }

    private TagId _tagId;
    private decimal _amount;
}
