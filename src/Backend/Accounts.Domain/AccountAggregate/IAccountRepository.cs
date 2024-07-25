using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.AccountAggregate.AccountEntity.ValueObjects;

namespace Accounts.Domain.AccountAggregate;

public interface IAccountRepository
{
    Task CreateAccountAsync(Account account);
    Task DeleteAccountAsync(AccountId accountId);
}