using Accounts.Domain.AccountAggregate.TransactionEntity;

namespace Accounts.Application.Transaction.AddTransactionCommand;

public record AddTransactionDto(TransactionId Id);
