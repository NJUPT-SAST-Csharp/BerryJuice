using Shared.Primitives.Query;

namespace Accounts.Application.AccountService.GetAccounts;

public sealed class GetAccountsQueryHandler(IGetAccountsRepository getAccountsRepository)
    : IQueryRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
    private readonly IGetAccountsRepository _getAccountsRepository = getAccountsRepository;

    public async Task<IEnumerable<AccountDto>> Handle(
        GetAccountsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _getAccountsRepository.GetAccountsByAdminAsync(cancellationToken);
    }
}
