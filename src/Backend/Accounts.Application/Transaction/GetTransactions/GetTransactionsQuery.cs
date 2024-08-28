using Accounts.Domain.AccountAggregate.AccountEntity;
using Shared.Primitives.Query;

namespace Accounts.Application.Transaction.GetTransactionsCommand;

public sealed class GetTransactionsQuery(long accountId)
    : IQueryRequest<IEnumerable<TransactionDto>>
{
    public AccountId AccountId { get; } = new(accountId);
}
