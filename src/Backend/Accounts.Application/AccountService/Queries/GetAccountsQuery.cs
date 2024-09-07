using Shared.Primitives.Query;

namespace Accounts.Application.AccountService.GetAccounts;

public sealed class GetAccountsQuery : IQueryRequest<IEnumerable<AccountDto>> { }

public interface IGetAccountsRepository
{
    public Task<IEnumerable<AccountDto>> GetAccountsByAdminAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAccountsQueryHandler(
    IGetAccountsRepository getAccountsRepository
) : IQueryRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
    private readonly IGetAccountsRepository _getAccountsRepository = getAccountsRepository;

    public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return await _getAccountsRepository.GetAccountsByAdminAsync(cancellationToken);
    }
}

// TODO: Implement DTO
