using Budget.Domain.BudgetAggregate;
using Budget.Domain.BudgetAggregate.BudgetEntity;
using Budget.Infrastructure.Persistence;
using Exceptions.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.DomainRepository;

internal sealed class BudgetRepository(BudgetContext context) : IBudgetRepository
{
    private readonly BudgetContext _context = context;

    /// <inheritdoc />
    public async Task<Domain.BudgetAggregate.BudgetEntity.Budget> GetBudgetAsync(
        BudgetId id,
        CancellationToken cancellationToken = default
    )
    {
        var a = await _context
            .Budgets.AsNoTracking()
            .SingleAsync(b => b.Id == id, cancellationToken: cancellationToken);

        return a
            ?? throw new DbNotFoundException(
                nameof(Domain.BudgetAggregate.BudgetEntity.Budget),
                id.Value.ToString()
            );
    }

    /// <inheritdoc />
    public async Task<BudgetId> AddBudgetAsync(
        Domain.BudgetAggregate.BudgetEntity.Budget budget,
        CancellationToken cancellationToken = default
    )
    {
        var a = await _context.Budgets.AddAsync(budget, cancellationToken);
        return a.Entity.Id;
    }
}
