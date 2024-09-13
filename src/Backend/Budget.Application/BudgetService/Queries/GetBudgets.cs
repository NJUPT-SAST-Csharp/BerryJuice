using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Query;

namespace Budget.Application.BudgetService.Queries;

public sealed class GetBudgetsQuery() : IQueryRequest<GetBudgetsDto>
{
    
}

public record GetBudgetsDto();

public interface IGetBudgetRepository { }

[UsedImplicitly]
internal sealed class GetBudgetsQueryHandler(
    [FromKeyedServices("")] IUnitOfWork unitOfWork,
    IGetBudgetRepository budgetRepository
) : IQueryRequestHandler<GetBudgetsQuery, GetBudgetsDto>
{
    public async Task<GetBudgetsDto> Handle(GetBudgetsQuery request, CancellationToken canecllationToken)
    {
        return new GetBudgetsDto();
    }
}
