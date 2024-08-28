using Accounts.Domain.AccountAggregate.AccountEntity;
using Accounts.Domain.AccountAggregate.TransactionEntity;
using Accounts.Domain.TagEntity;
using Primitives.Command;

namespace Accounts.Application.Transaction.AddTransactionCommand;

public sealed class AddTransactionCommand(
    AccountId id,
    TransactionAmount amount,
    TagId[] tags,
    TransactionDescription description
) : ICommandRequest<AddTransactionDto>
{
    public AccountId AccountId { get; } = id;

    public TransactionAmount TransactionAmount { get; } = amount;

    public TagId[] Tags { get; } = tags;

    public TransactionDescription Description { get; } = description;
}
