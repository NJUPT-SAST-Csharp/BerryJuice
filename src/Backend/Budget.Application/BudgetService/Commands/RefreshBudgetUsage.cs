using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Budget.Application.BudgetService.Commands;

public sealed class RefreshBudgetUsageCommand() : ICommandRequest<RefreshBudgetUsageDto>
{
    
}

public record RefreshBudgetUsageDto();

public interface IRefreshBudgetUsageRepository { }

[UsedImplicitly]
internal sealed class RefreshBudgetUsageCommandHandler(
    [FromKeyedServices("")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<RefreshBudgetUsageCommand, RefreshBudgetUsageDto>
{
    public async Task<RefreshBudgetUsageDto> Handle(
        RefreshBudgetUsageCommand request,
        CancellationToken canecllationToken
    )
    {
        return new RefreshBudgetUsageDto();
    }
}


    
    

