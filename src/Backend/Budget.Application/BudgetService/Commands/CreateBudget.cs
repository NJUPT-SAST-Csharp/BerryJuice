using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Budget.Application.BudgetService.Commands;

public sealed class CreateBudgetCommand() : ICommandRequest<CreateBudgetDto>
{
    
}

public record CreateBudgetDto();

public interface ICreateBudgetRepository { }

[UsedImplicitly]
internal sealed class CreateBudgetCommandHandler(
    [FromKeyedServices("budget")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<CreateBudgetCommand, CreateBudgetDto>
{
    public async Task<CreateBudgetDto> Handle(CreateBudgetCommand request, CancellationToken canecllationToken)
    {
        return new CreateBudgetDto();
    }
}
