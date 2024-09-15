using Budget.Domain.BudgetAggregate;
using Budget.Domain.BudgetAggregate.BudgetEntity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Budget.Application.BudgetService.Commands;

public sealed class RefreshBudgetUsageCommand(long budgetId, decimal usedAmount)
    : ICommandRequest<RefreshBudgetUsageDto>
{
    internal BudgetId BudgetId { get; } = new(budgetId);
    internal decimal Amount { get; } = usedAmount;
}

public record RefreshBudgetUsageDto(decimal usedAmount, decimal usageLimit);

[UsedImplicitly]
internal sealed class RefreshBudgetUsageCommandHandler(
    [FromKeyedServices("")] IUnitOfWork unitOfWork,
    IBudgetRepository budgetRepository
) : ICommandRequestHandler<RefreshBudgetUsageCommand, RefreshBudgetUsageDto>
{
    public async Task<RefreshBudgetUsageDto> Handle(
        RefreshBudgetUsageCommand request,
        CancellationToken cancellationToken
    )
    {
        var budget = await budgetRepository.GetBudgetAsync(request.BudgetId, cancellationToken);
        var amount = budget.UpdateUsage(request.Amount);
        await unitOfWork.CommitChangesAsync(cancellationToken);
        return new RefreshBudgetUsageDto(amount.Used, amount.Limit);
    }
}
