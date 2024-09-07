using Accounts.Application.TransactionService.Models;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Shared.Primitives.Query;

namespace Accounts.Application.TransactionService.Queries;

public sealed class GetTransactionsQuery(
    long accountId
) : IQueryRequest<IEnumerable<TransactionModel>>
{
    public AccountId AccountId { get; } = new(accountId);
}

// TODO: Implement DTO
public interface IGetTransactionRepository
{
    public Task<IEnumerable<TransactionModel>> GetTransactionsByAdminAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    );
}

public sealed class GetTransactionQueryHandler(
    IGetTransactionRepository repo
) : IQueryRequestHandler<GetTransactionsQuery, IEnumerable<TransactionModel>>
{
    public Task<IEnumerable<TransactionModel>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return repo.GetTransactionsByAdminAsync(request.AccountId, cancellationToken);
    }
}
