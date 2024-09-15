using System.Data;
using Accounts.Application.TagService;
using Accounts.Application.TagService.Models;
using Accounts.Application.TransactionService;
using Accounts.Application.TransactionService.Models;
using Accounts.Application.TransactionService.Queries;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Dapper;
using Primitives.QueryDatabase;

namespace Accounts.Infrastructure.QueryRepository;

internal class TransactionQueryRepository(
    IDbConnectionFactory factory
) : IGetTransactionRepository
{
    private readonly IDbConnection _connection = factory.GetConnection();

    public async Task<IEnumerable<TransactionModel>> GetTransactionsByAdminAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    )
    {
        const string sql1 = """
            
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
                        
            """;

        const string sql2 = """
            
                            SELECT  t.id as TransactionId, 
                                    t.amount as Amount, 
                                    t.currency as CurrencyType, 
                                    t.description as Description, 
                                    t.created_at as TimeCreated
                            FROM bj_accounts.transactions t
                            WHERE t.account_id = @AccountId
                        
            """;

        var transactions = await _connection.QueryAsync<TransactionModel>(sql2, new { AccountId = id.Value });

        var transactions_tag = await _connection.QueryAsync<TransactionModel, TagModel, TransactionModel>(
            sql1,
            map: (transaction, tag) =>
            {
                transaction.Tags.Add(tag);
                return transaction;
            },
            new { AccountId = id.Value },
            splitOn: "TagId"
        );

        foreach (var tagEntry in transactions_tag)
        {
            var transaction = transactions.FirstOrDefault(predicate: t => t.TransactionId == tagEntry.TransactionId);
            transaction?.Tags.Add(tagEntry.Tags.First());
        }

        //var result = transactions_tag
        //    .GroupBy(t => t.TransactionId)
        //    .Select(g =>
        //    {
        //        var transaction = g.First() with
        //        {
        //            Tags = g.Select(t => new TagDto(t.Tags.First().TagId, t.Tags.First().TagName))
        //                .ToList()
        //        };
        //        return transaction;
        //    });

        return transactions;
    }
}
