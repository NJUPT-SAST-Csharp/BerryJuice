using Accounts.Domain.AccountAggregate;
using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Enums;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Accounts.Application.TransactionService.Commands;

public sealed class AddTransactionCommand(
    long id,
    CurrencyType currency,
    decimal amount,
    long[] tags,
    string description
) : ICommandRequest<AddTransactionDto>
{
    public AccountId AccountId { get; } = new(id);

    public TransactionAmount TransactionAmount { get; } = new(currency, amount);

    public TagId[] Tags { get; } = Array.ConvertAll(tags, converter: t => new TagId(t));

    public TransactionDescription Description { get; } = new(description);
}

public record AddTransactionDto(
    long Id
);

[UsedImplicitly]
public sealed class AddTransactionCommandHandler(
    IAccountRepository accountRepo,
    ITagRepository tagRepo,
    [FromKeyedServices(key: "accounts")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<AddTransactionCommand, AddTransactionDto>
{
    public async Task<AddTransactionDto> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepo.GetAccountAsync(request.AccountId, cancellationToken);

        List<Tag> tags = [];

        foreach (var t in request.Tags)
        {
            var tag = await tagRepo.GetTagAsync(t, cancellationToken);
            tags.Add(tag);
        }

        var id = account.AddTransaction(request.TransactionAmount, DateTime.UtcNow, request.Description, tags);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return new AddTransactionDto(id.Value);
    }
}
