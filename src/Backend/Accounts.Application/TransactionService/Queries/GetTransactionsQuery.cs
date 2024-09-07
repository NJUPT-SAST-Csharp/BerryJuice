using Accounts.Domain.AccountAggregate.AccountEntity;
using Shared.Primitives.Query;

namespace Accounts.Application.TransactionService.GetTransactions;

public sealed class GetTransactionsQuery(
    long accountId
) : IQueryRequest<IEnumerable<TransactionDto>>
{
    public AccountId AccountId { get; } = new(accountId);
}

// TODO: Implement DTO
public interface IGetTransactionRepository
{
    public Task<IEnumerable<TransactionDto>> GetTransactionsByAdminAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    );
}

public sealed class GetTransactionQueryHandler(
    IGetTransactionRepository repo
) : IQueryRequestHandler<GetTransactionsQuery, IEnumerable<TransactionDto>>
{
    private readonly IGetTransactionRepository _repo = repo;

    public Task<IEnumerable<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return _repo.GetTransactionsByAdminAsync(request.AccountId, cancellationToken);
    }
}
