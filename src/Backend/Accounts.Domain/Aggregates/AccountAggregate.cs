using Accounts.Domain.Entities;
using Accounts.Domain.ValueObjects;

namespace Accounts.Domain.Aggregates;

public class AccountAggregate(IAccountRepository accountRepository)
{
    private readonly IAccountRepository _accountRepository= accountRepository;
    
    public async Task<Account> CreateAccountAsync(DateTime date, decimal amount,MethodOfPayment methodOfPayment, string tag, string? description)
    {
        var account = new Account(date, amount,methodOfPayment, tag, description);
        await _accountRepository.CreateAccountAsync(account);
        return account;
    }
    public async Task DeleteAccountAsync(AccountId accountId)
    {
        await _accountRepository.DeleteAccountAsync(accountId);
    }
}