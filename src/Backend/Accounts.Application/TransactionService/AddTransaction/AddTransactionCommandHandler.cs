using Accounts.Domain.AccountAggregate;
using Accounts.Domain.TagEntity;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Accounts.Application.TransactionService.AddTransaction;

public sealed class AddTransactionCommandHandler(
    IAccountRepository accountRepo,
    ITagRepository tagRepo,
    [FromKeyedServices("accounts")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<AddTransactionCommand, AddTransactionDto>
{
    private readonly IAccountRepository _accountRepo = accountRepo;
    private readonly ITagRepository _tagRepo = tagRepo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AddTransactionDto> Handle(
        AddTransactionCommand request,
        CancellationToken cancellationToken
    )
    {
        var account = await _accountRepo.GetAccountAsync(request.AccountId, cancellationToken);

        List<Tag> tags = [];

        foreach (var t in request.Tags)
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
