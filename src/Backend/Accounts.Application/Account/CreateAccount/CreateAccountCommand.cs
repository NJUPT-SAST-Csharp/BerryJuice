using Accounts.Domain.AccountAggregate.AccountEntity;
using Primitives.Command;

namespace Accounts.Application.Account.CreateAccount;

public sealed class CreateAccountCommand(AccountDescription description)
    : ICommandRequest<CreateAccountDto>
{
    public AccountDescription Description { get; } = description;
}
