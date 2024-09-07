using Accounts.Domain.AccountAggregate;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Accounts.Application.AccountService.CreateAccount;

public sealed class CreateAccountCommand (string description) : ICommandRequest<CreateAccountDto>
{
    public AccountDescription Description { get; } = new(description);
}

public record CreateAccountDto (long Id);
internal sealed class CreateAccountCommandHandler (
    IAccountRepository accountRepository,
    [FromKeyedServices("accounts")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<CreateAccountCommand, CreateAccountDto>
{
    private IAccountRepository _accountRepository = accountRepository;
    private IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateAccountDto> Handle (
        CreateAccountCommand request,
        CancellationToken cancellationToken
    )
    {
        var account = Account.CreateNewAccount(request.Description);
        var newId = await _accountRepository.AddAccountAsync(account, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);
        return new CreateAccountDto(newId.Value);
    }
}
