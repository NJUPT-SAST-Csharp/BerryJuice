using System.Data;
using Accounts.Application.TagService;
using Accounts.Application.TransactionService;
using Accounts.Application.TransactionService.GetTransactions;
using Dapper;
using Primitives.QueryDatabase;

namespace Accounts.Infrastructure.QueryRepository;

internal class TransactionQueryRepository(IDbConnectionFactory factory) : IGetTransactionRepository
{
    private readonly IDbConnection _connection = factory.GetConnection();

    public Task<IEnumerable<TransactionDto>> GetTransactionsByAdminAsync(
        CancellationToken cancellationToken = default
    )
    {
        const string sql =
            @"
                SELECT  t.id as Id, 
                        t.amount as Amount, 
                        t.currency as CurrencyType, 
                        t.description as Description, 
                        t.created_at as TimeCreated 
                FROM BJAccounts.transactions t
                INNER JOIN BJAccounts.transactions_tags tt ON tt.transaction_id = t.id
                INNER JOIN BJAccounts.tags tags ON tags.id = tt.tag_id
            ";

        return _connection.QueryAsync<TransactionDto, TagDto, TransactionDto>(
            sql,
            (transaction, tag) =>
            {
                transaction.Tags.Add(tag);
                return transaction;
            },
            splitOn: "Id"
        );
    }
}
