namespace Accounts.Application.AccountService.GetAccounts;

public interface IGetAccountsRepository
{
    public Task<IEnumerable<AccountDto>> GetAccountsByAdminAsync(
        CancellationToken cancellationToken = default
    );
}
