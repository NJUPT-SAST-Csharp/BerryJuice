using Budget.Application.BudgetService.Model;
using Budget.Domain.BudgetAggregate.BudgetEntity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Query;

namespace Budget.Application.BudgetService.Queries;

public sealed class GetBudgetsQuery(long accountId) : IQueryRequest<GetBudgetsDto>
{
    internal AccountId AccountId { get; } = new AccountId(accountId);
}

public record GetBudgetsDto(List<BudgetModel> Budgets);

public interface IGetBudgetRepository
{
    public Task<IEnumerable<BudgetModel>> GetBudgetsByAdminAsync(
        AccountId requestAccountId,
        CancellationToken cancellationToken = default
    );
}

[UsedImplicitly]
internal sealed class GetBudgetsQueryHandler(
    [FromKeyedServices("budget")] IUnitOfWork unitOfWork,
    IGetBudgetRepository repository
) : IQueryRequestHandler<GetBudgetsQuery, GetBudgetsDto>
{
    public async Task<GetBudgetsDto> Handle(GetBudgetsQuery request, CancellationToken canecllationToken)
    {
        var list = await repository.GetBudgetsByAdminAsync(request.AccountId, canecllationToken);
        return new GetBudgetsDto(list.ToList());
    }
}
