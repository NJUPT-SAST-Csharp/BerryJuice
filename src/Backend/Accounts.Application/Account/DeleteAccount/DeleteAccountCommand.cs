using Accounts.Domain.AccountAggregate.AccountEntity;

namespace Accounts.Application.Account.DeleteAccount;

public sealed class DeleteAccountCommand(AccountId id)
{
    public AccountId Id { get; } = id;
}
