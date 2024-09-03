using Accounts.Application.TransactionService;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Shared.Primitives.Query;

namespace Accounts.Application.TransactionService.GetTransactions;

public sealed class GetTransactionsQuery(long accountId)
    : IQueryRequest<IEnumerable<TransactionDto>>
{
    public AccountId AccountId { get; } = new(accountId);
}
