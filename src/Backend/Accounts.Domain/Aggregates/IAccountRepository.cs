using Accounts.Domain.Entities;
using Accounts.Domain.ValueObjects;

namespace Accounts.Domain.Aggregates;

public interface IAccountRepository
{
    Task CreateAccountAsync(Account account);
    Task DeleteAccountAsync(AccountId accountId);
}