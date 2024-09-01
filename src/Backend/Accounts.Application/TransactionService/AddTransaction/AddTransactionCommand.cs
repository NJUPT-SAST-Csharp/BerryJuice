using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Enums;
using Primitives.Command;

namespace Accounts.Application.TransactionService.AddTransaction;

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

    public TagId[] Tags { get; } = Array.ConvertAll(tags, t => new TagId(t));

    public TransactionDescription Description { get; } = new(description);
}
