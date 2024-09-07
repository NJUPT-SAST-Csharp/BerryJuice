using Accounts.Application.AccountService.Models;
using Shared.Primitives.Query;

namespace Accounts.Application.AccountService.Queries;

public sealed class GetAccountsQuery : IQueryRequest<IEnumerable<AccountModel>> { }

public interface IGetAccountsRepository
{
    public Task<IEnumerable<AccountModel>> GetAccountsByAdminAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAccountsQueryHandler(
    IGetAccountsRepository getAccountsRepository
) : IQueryRequestHandler<GetAccountsQuery, IEnumerable<AccountModel>>
{
    public async Task<IEnumerable<AccountModel>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return await getAccountsRepository.GetAccountsByAdminAsync(cancellationToken);
    }
}

// TODO: Implement DTO
