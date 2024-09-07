using System.Data;
using Accounts.Application.AccountService;
using Accounts.Application.AccountService.Models;
using Accounts.Application.AccountService.Queries;
using Dapper;
using Primitives.QueryDatabase;

namespace Accounts.Infrastructure.QueryRepository;

internal sealed class AccountQueryRepository(
    IDbConnectionFactory factory
) : IGetAccountsRepository
{
    private readonly IDbConnection _connection = factory.GetConnection();

    public Task<IEnumerable<AccountModel>> GetAccountsByAdminAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
                SELECT id as Id, description as Description
                FROM bj_accounts.accounts
            ";

        return _connection.QueryAsync<AccountModel>(sql);
    }
}
