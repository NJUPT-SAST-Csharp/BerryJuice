using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Budget.Application.BudgetService.Commands;

public sealed class EditBudgetCommand() : ICommandRequest<EditBudgetDto>
{
    
}

public record EditBudgetDto();

[UsedImplicitly]
internal sealed class EditBudgetCommandHandler(
    [FromKeyedServices("")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<EditBudgetCommand, EditBudgetDto>
{
    public async Task<EditBudgetDto> Handle(EditBudgetCommand request, CancellationToken canecllationToken)
    {
        return new EditBudgetDto();
    }
}
