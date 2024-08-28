using Accounts.Domain.AccountAggregate.AccountEntity;
using Primitives.Command;

namespace Accounts.Application.Account.CreateAccount;

public sealed class CreateAccountCommand(string description) : ICommandRequest<CreateAccountDto>
{
    public AccountDescription Description { get; } = new(description);
}
