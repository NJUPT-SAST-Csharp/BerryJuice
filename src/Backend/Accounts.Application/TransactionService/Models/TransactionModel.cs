using Accounts.Application.TagService.Models;
using Enums;

namespace Accounts.Application.TransactionService.Models;

public record TransactionModel(
    long TransactionId,
    long Amount,
    CurrencyType CurrencyType,
    string Description,
    DateTime TimeCreated,
    List<TagModel> Tags
)
{
    public TransactionModel() : this(0, 0, CurrencyType.USD, null, new DateTime(), new List<TagModel>()) { }

    public required long TransactionId { get; init; } = TransactionId;
    public required long Amount { get; init; } = Amount;
    public required CurrencyType CurrencyType { get; init; } = CurrencyType;
    public required string Description { get; init; } = Description;
    public required DateTime TimeCreated { get; init; } = TimeCreated;
    public required List<TagModel> Tags { get; init; } = Tags;
}
