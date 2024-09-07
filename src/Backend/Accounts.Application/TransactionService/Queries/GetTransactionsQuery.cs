using Accounts.Application.TransactionService.Models;
using Accounts.Domain.AccountAggregate.AccountEntity;
using JetBrains.Annotations;
using Shared.Primitives.Query;

namespace Accounts.Application.TransactionService.Queries;

public sealed class GetTransactionsQuery(
    long accountId
) : IQueryRequest<GetTransactionDto>
{
    public AccountId AccountId { get; } = new(accountId);
}

public record GetTransactionDto(
    IEnumerable<TransactionModel> Transactions
);

public interface IGetTransactionRepository
{
    public Task<IEnumerable<TransactionModel>> GetTransactionsByAdminAsync(
        AccountId id,
        CancellationToken cancellationToken = default
    );
}

[UsedImplicitly]
public sealed class GetTransactionQueryHandler(
    IGetTransactionRepository repo
) : IQueryRequestHandler<GetTransactionsQuery, GetTransactionDto>
{
    public async Task<GetTransactionDto> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return new GetTransactionDto(await repo.GetTransactionsByAdminAsync(request.AccountId, cancellationToken));
    }
}
