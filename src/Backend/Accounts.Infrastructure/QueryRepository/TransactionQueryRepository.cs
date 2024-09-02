using System.Data;
using Accounts.Application.TagService;
using Accounts.Application.TransactionService;
using Accounts.Application.TransactionService.GetTransactions;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Dapper;
using Primitives.QueryDatabase;

namespace Accounts.Infrastructure.QueryRepository;

internal class TransactionQueryRepository(IDbConnectionFactory factory) : IGetTransactionRepository
{
    private readonly IDbConnection _connection = factory.GetConnection();

    public async Task<IEnumerable<TransactionDto>> GetTransactionsByAdminAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    )
    {
        const string sql1 =
            @"
                SELECT  t.id as TransactionId,
                        t.amount as Amount, 
                        t.currency as CurrencyType, 
                        t.description as Description, 
                        t.created_at as TimeCreated,
                        tags.id as TagId,
                        tags.name as TagName
                FROM bj_accounts.transactions t
                INNER JOIN bj_accounts.transactions_tags tt ON tt.transaction_id = t.id
                INNER JOIN bj_accounts.tags tags ON tags.id = tt.tag_id
                WHERE t.account_id = @AccountId
            ";

        const string sql2 =
            @"
                SELECT  t.id as Id, 
                        t.amount as Amount, 
                        t.currency as CurrencyType, 
                        t.description as Description, 
                        t.created_at as TimeCreated,
                        tags.id as TagId,
                        tags.name as TagName
                FROM bj_accounts.transactions t
                INNER JOIN bj_accounts.transactions_tags tt ON tt.transaction_id = t.id
                INNER JOIN bj_accounts.tags tags ON tags.id = tt.tag_id
                WHERE t.account_id = @AccountId
            ";

        var transactions = await _connection.QueryAsync<TransactionDto, TagDto, TransactionDto>(
            sql1,
            (transaction, tag) =>
            {
                transaction.Tags.Add(tag);
                return transaction;
            },
            new { AccountId = id.Value },
            splitOn: "TagId"
        );

        var result = transactions
            .GroupBy(t => t.TransactionId)
            .Select(g =>
            {
                var transaction = g.First() with
                {
                    Tags = g.Select(t => new TagDto(t.Tags.First().TagId, t.Tags.First().TagName))
                        .ToList()
                };
                return transaction;
            });

        return result;
    }
}
