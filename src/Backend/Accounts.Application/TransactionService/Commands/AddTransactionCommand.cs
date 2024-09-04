using Accounts.Domain.AccountAggregate;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Enums;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Accounts.Application.TransactionService.AddTransaction;

public sealed class AddTransactionCommand (
    long id,
    CurrencyType currency,
    decimal amount,
    long[ ] tags,
    string description
) : ICommandRequest<AddTransactionDto>
{
    public AccountId AccountId { get; } = new(id);

    public TransactionAmount TransactionAmount { get; } = new(currency, amount);

    public TagId[ ] Tags { get; } = Array.ConvertAll(tags, t => new TagId(t));

    public TransactionDescription Description { get; } = new(description);
}

public record AddTransactionDto (long Id);

public sealed class AddTransactionCommandHandler (
    IAccountRepository accountRepo,
    ITagRepository tagRepo,
    [FromKeyedServices("accounts")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<AddTransactionCommand, AddTransactionDto>
{
    private readonly IAccountRepository _accountRepo = accountRepo;
    private readonly ITagRepository _tagRepo = tagRepo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AddTransactionDto> Handle (
        AddTransactionCommand request,
        CancellationToken cancellationToken
    )
    {
        var account = await _accountRepo.GetAccountAsync(request.AccountId, cancellationToken);

        List<Tag> tags = [];

        foreach ( var t in request.Tags )
        {
            var tag = await _tagRepo.GetTagAsync(t, cancellationToken);
            tags.Add(tag);
        }

        var id = account.AddTransaction(
            request.TransactionAmount,
            DateTime.UtcNow,
            request.Description,
            tags
        );

        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return new AddTransactionDto(id.Value);
    }
}

