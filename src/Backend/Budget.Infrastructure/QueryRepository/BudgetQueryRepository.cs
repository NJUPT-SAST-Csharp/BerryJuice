using System.Data;
using Budget.Application.BudgetService.Model;
using Budget.Application.BudgetService.Queries;
using Budget.Domain.BudgetAggregate.BudgetEntity;
using Dapper;
using Primitives.QueryDatabase;

namespace Budget.Infrastructure.QueryRepository;

internal sealed class BudgetQueryRepository(
    IDbConnectionFactory factory
) : IGetBudgetRepository
{
    private readonly IDbConnection _connection = factory.GetConnection();

    /// <inheritdoc />
    public Task<IEnumerable<BudgetModel>> GetBudgetsByAdminAsync(
        AccountId requestAccountId,
        CancellationToken cancellationToken = default
    )
    {
        const string sql = """
                            SELECT id as Id, 
                                   amount as Amount,
                                   used as Used,
                                   description as Description,
                                   duration as Duration,
                                   begin_date as BeginDate
                            FROM bj_budget.budgets
                            WHERE  account_id = @AccountId
                        
            """;

        return _connection.QueryAsync<BudgetModel>(sql, param: new { AccoundId = requestAccountId.Value });
    }
}
