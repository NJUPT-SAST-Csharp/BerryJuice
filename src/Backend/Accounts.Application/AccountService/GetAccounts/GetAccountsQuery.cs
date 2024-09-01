using Shared.Primitives.Query;

namespace Accounts.Application.AccountService.GetAccounts;

public sealed class GetAccountsQuery() : IQueryRequest<IEnumerable<AccountDto>> { }
