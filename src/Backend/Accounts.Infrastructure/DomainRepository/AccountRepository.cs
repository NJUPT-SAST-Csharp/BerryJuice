using Accounts.Domain.AccountAggregate;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Infrastructure.Persistence;
using Exceptions.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.DomainRepository;

internal class AccountRepository(AccountsContext context) : IAccountRepository
{
    private readonly AccountsContext _context = context;

    public async Task<AccountId> AddAccountAsync(
        Account account,
        CancellationToken cancellationToken = default
    )
    {
        var a = await _context.Accounts.AddAsync(account, cancellationToken);
        return a.Entity.Id;
    }

    public async Task<Account> GetAccountAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    )
    {
        var a = await _context
            .Accounts.Where<Account>(a => a.Id == id)
            .Include("_transactions")
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return a ?? throw new DbNotFoundException(nameof(Account), id.Value.ToString());
    }
}
