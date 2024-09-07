using Accounts.Application.AccountService.Models;
using JetBrains.Annotations;
using Primitives.Query;

namespace Accounts.Application.AccountService.Queries;

public sealed class GetAccountsQuery : IQueryRequest<GetAccountsDto> { }

public record GetAccountsDto(
    IEnumerable<AccountModel> Accounts
);

public interface IGetAccountsRepository
{
    public Task<IEnumerable<AccountModel>> GetAccountsByAdminAsync(CancellationToken cancellationToken = default);
}

[UsedImplicitly]
public sealed class GetAccountsQueryHandler(
    IGetAccountsRepository getAccountsRepository
) : IQueryRequestHandler<GetAccountsQuery, GetAccountsDto>
{
    public async Task<GetAccountsDto> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return new GetAccountsDto(await getAccountsRepository.GetAccountsByAdminAsync(cancellationToken));
    }
}
