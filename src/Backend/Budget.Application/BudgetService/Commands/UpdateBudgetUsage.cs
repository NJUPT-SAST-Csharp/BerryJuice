using Budget.Domain.BudgetAggregate.BudgetEntity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Budget.Application.BudgetService.Commands;

public sealed class UpdateBudgetUsageCommand(long accountId, decimal amountChange)
    : ICommandRequest<UpdateBudgetUsageDto>
{
    private BudgetId AccountId { get; } = new(accountId);
    private decimal AmountChange { get; } = amountChange;
}

public record UpdateBudgetUsageDto();

[UsedImplicitly]
internal sealed class UpdateBudgetUsageCommandHandler(
    [FromKeyedServices("")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<UpdateBudgetUsageCommand, UpdateBudgetUsageDto>
{
    public async Task<UpdateBudgetUsageDto> Handle(
        UpdateBudgetUsageCommand request,
        CancellationToken canecllationToken
    )
    {
        return new UpdateBudgetUsageDto();
    }
}
