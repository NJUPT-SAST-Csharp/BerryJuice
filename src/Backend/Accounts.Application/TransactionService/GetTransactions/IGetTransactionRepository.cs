using Accounts.Application.TransactionService;

namespace Accounts.Application.TransactionService.GetTransactions;

public interface IGetTransactionRepository
{
    public Task<IEnumerable<TransactionDto>> GetTransactionsByAdminAsync(
        CancellationToken cancellationToken = default
    );
}
