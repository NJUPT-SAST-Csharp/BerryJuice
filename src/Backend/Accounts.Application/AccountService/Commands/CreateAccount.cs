using Accounts.Domain.AccountAggregate;
using Accounts.Domain.AccountAggregate.AccountEntity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Accounts.Application.AccountService.Commands;

public sealed class CreateAccountCommand(
    string description
) : ICommandRequest<CreateAccountDto>
{
    internal AccountDescription Description { get; } = new(description);
}

public record CreateAccountDto(
    long Id
);

[UsedImplicitly]
internal sealed class CreateAccountCommandHandler(
    IAccountRepository accountRepository,
    [FromKeyedServices(key: "accounts")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<CreateAccountCommand, CreateAccountDto>
{
    public async Task<CreateAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = Account.CreateNewAccount(request.Description);
        var newId = await accountRepository.AddAccountAsync(account, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);
        return new CreateAccountDto(newId.Value);
    }
}
