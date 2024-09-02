using Accounts.Domain.AccountAggregate.AccountEntity;

namespace Accounts.Application.TransactionService.GetTransactions;

public interface IGetTransactionRepository
{
    public Task<IEnumerable<TransactionDto>> GetTransactionsByAdminAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    );
}
