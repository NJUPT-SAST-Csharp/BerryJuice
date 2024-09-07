using Accounts.Domain.AccountAggregate.AccountEntity;

namespace Accounts.Application.AccountService.DeleteAccount;

public sealed class DeleteAccountCommand(
    long id
)
{
    public AccountId Id { get; } = new(id);
}
