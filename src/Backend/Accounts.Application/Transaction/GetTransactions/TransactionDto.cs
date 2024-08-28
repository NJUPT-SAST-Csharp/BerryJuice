using Enums;

namespace Accounts.Application.Transaction.GetTransactionsCommand;

public record TransactionDto(
    long Amount,
    CurrencyType CurrencyType,
    string Description,
    DateTime TimeCreated,
    long[] TagIds
);
