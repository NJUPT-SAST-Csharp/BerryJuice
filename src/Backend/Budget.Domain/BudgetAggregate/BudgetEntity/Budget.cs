using Budget.Domain.BudgetAggregate.BudgetEntity.Events;
using Primitives.Entity;
using Utilities;

namespace Budget.Domain.BudgetAggregate.BudgetEntity;

public class Budget : EntityBase<BudgetId>, IAggregateRoot<Budget>
{
    private BudgetAmount _amount;
    private BudgetDescription _description;
    private BudgetDuration _duration;

    private AccountId _accountId;

    private Budget(BudgetAmount amount, BudgetDescription description, BudgetDuration duration, AccountId accountId)
        : base(new BudgetId(SnowFlakeIdGenerator.NewId))
    {
        _amount = amount;
        _description = description;
        _duration = duration;
        _accountId = accountId;
    }

    public static Budget CreateBudget(
        BudgetAmount amount,
        BudgetDescription description,
        BudgetDuration duration,
        AccountId accountId
    )
    {
        var budget = new Budget(amount, description, duration, accountId);
        budget.AddDomainEvent(new BudgetCreatedDomainEvent(budget.Id));
        return budget;
    }

    public BudgetAmount AddUsage(decimal amount)
    {
        _amount = new BudgetAmount(Used: _amount.Used + amount, Limit: _amount.Limit);
        return _amount;
    }

    public BudgetAmount UpdateUsage(decimal amount)
    {
        _amount = new BudgetAmount(Used: amount, Limit: _amount.Limit);
        return _amount;
    }
}
