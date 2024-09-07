using Accounts.Application.TagService;
using Enums;

namespace Accounts.Application.TransactionService;

public record TransactionDto
{
    public TransactionDto()
    {
        Tags = new List<TagDto>();
    }

    public TransactionDto(
        long transactionId,
        long amount,
        CurrencyType currencyType,
        string description,
        DateTime timeCreated,
        List<TagDto> tags
    )
    {
        TransactionId = transactionId;
        Amount = amount;
        CurrencyType = currencyType;
        Description = description;
        TimeCreated = timeCreated;
        Tags = tags;
    }

    public required long TransactionId { get; init; }
    public required long Amount { get; init; }
    public required CurrencyType CurrencyType { get; init; }
    public required string Description { get; init; }
    public required DateTime TimeCreated { get; init; }
    public required List<TagDto> Tags { get; init; }
}
