using Accounts.Application.TagService;
using Enums;

namespace Accounts.Application.TransactionService;

public record TransactionDto(
    long Id,
    long Amount,
    CurrencyType CurrencyType,
    string Description,
    DateTime TimeCreated,
    List<TagDto> Tags
);
