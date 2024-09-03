using Accounts.Domain.AccountAggregate.AccountEntity;

namespace Accounts.Domain.AccountAggregate;

public interface IAccountRepository
{
    public Task<Account> GetAccountAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    );

    public Task<AccountId> AddAccountAsync(
        Account account,
        CancellationToken cancellationToken = default
    );
}
