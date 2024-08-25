using Accounts.Domain.AccountAggregate.AccountEntity;

namespace Accounts.Domain.AccountAggregate;

public interface IAccountRepository
{
    Task CreateAccountAsync(Account account);
    Task DeleteAccountAsync(AccountId accountId);
}
