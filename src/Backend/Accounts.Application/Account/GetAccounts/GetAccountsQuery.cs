using Shared.Primitives.Query;

namespace Accounts.Application.Account.GetAccounts;

public sealed class GetAccountsQuery() : IQueryRequest<IEnumerable<AccountDto>> { }
