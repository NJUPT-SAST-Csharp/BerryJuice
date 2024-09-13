using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Budget.Application.BudgetService.Commands;

public sealed class UpdateBudgetUsageCommand() : ICommandRequest<UpdateBudgetUsageDto>
{
    
}

public record UpdateBudgetUsageDto();

[UsedImplicitly]
internal sealed class UpdateBudgetUsageCommandHandler(
    [FromKeyedServices("")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<UpdateBudgetUsageCommand, UpdateBudgetUsageDto>
{
    public async Task<UpdateBudgetUsageDto> Handle(
        UpdateBudgetUsageCommand request,
        CancellationToken canecllationToken
    )
    {
        return new UpdateBudgetUsageDto();
    }
}
