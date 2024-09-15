using Budget.Application.BudgetService.Queries;
using Budget.Domain.BudgetAggregate;
using Budget.Domain.BudgetAggregate.BudgetEntity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;
using Primitives.Query;

namespace Budget.Application.BudgetService.Commands;

public sealed class UpdateBudgetUsageCommand(
    long accountId,
    decimal amountChange
) : ICommandRequest<UpdateBudgetUsageDto>
{
    internal AccountId AccountId { get; } = new(accountId);
    internal decimal AmountChange { get; } = amountChange;
}

public record UpdateBudgetUsageDto();

[UsedImplicitly]
internal sealed class UpdateBudgetUsageCommandHandler(
    [FromKeyedServices("")] IUnitOfWork unitOfWork,
    IBudgetRepository repository,
    IQueryRequestSender querySender
) : ICommandRequestHandler<UpdateBudgetUsageCommand, UpdateBudgetUsageDto>
{
    public async Task<UpdateBudgetUsageDto> Handle(
        UpdateBudgetUsageCommand request,
        CancellationToken cancellationToken
    )
    {
        var query = new GetBudgetsQuery(request.AccountId.Value);
        var budget = await querySender.QueryAsync(query, cancellationToken);

        foreach (var budgetItem in budget.Budgets)
        {
            var b = await repository.GetBudgetAsync(new BudgetId(budgetItem.Id), cancellationToken);
            var amount = b.AddUsage(request.AmountChange);
        }

        await unitOfWork.CommitChangesAsync(cancellationToken);
        return new UpdateBudgetUsageDto();
    }
}
