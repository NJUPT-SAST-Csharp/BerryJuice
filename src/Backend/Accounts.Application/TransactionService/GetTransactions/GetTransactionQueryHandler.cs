using Shared.Primitives.Query;

namespace Accounts.Application.TransactionService.GetTransactions;

public sealed class GetTransactionQueryHandler(IGetTransactionRepository repo)
    : IQueryRequestHandler<GetTransactionsQuery, IEnumerable<TransactionDto>>
{
    private readonly IGetTransactionRepository _repo = repo;

    public Task<IEnumerable<TransactionDto>> Handle(
        GetTransactionsQuery request,
        CancellationToken cancellationToken
    )
    {
        return _repo.GetTransactionsByAdminAsync(cancellationToken);
    }
}
