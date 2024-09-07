using System.Data;
using Accounts.Application.AccountService;
using Accounts.Application.AccountService.GetAccounts;
using Dapper;
using Primitives.QueryDatabase;

namespace Accounts.Infrastructure.QueryRepository;

internal sealed class AccountQueryRepository(
    IDbConnectionFactory factory
) : IGetAccountsRepository
{
    private readonly IDbConnection _connection = factory.GetConnection();

    public Task<IEnumerable<AccountDto>> GetAccountsByAdminAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
                SELECT id as Id, description as Description
                FROM bj_accounts.accounts
            ";

        return _connection.QueryAsync<AccountDto>(sql);
    }
}
