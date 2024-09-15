﻿using Budget.Domain.BudgetAggregate;
using Budget.Domain.BudgetAggregate.BudgetEntity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using Primitives.Command;

namespace Budget.Application.BudgetService.Commands;

public sealed class CreateBudgetCommand(
    TimeSpan duration,
    DateOnly beginDate,
    decimal amount,
    string description,
    long accountId
) : ICommandRequest<CreateBudgetDto>
{
    internal BudgetAmount Amount { get; set; } = new(Used: 0, Limit: amount);
    internal BudgetDescription Description { get; set; } = new(description);
    internal BudgetDuration Duration { get; set; } = new(duration, beginDate);
    internal AccountId AccountId { get; set; } = new(accountId);
}

public record CreateBudgetDto(
    long Id
);

[UsedImplicitly]
internal sealed class CreateBudgetCommandHandler(
    IBudgetRepository budgetRepository,
    [FromKeyedServices(key: "budget")] IUnitOfWork unitOfWork
) : ICommandRequestHandler<CreateBudgetCommand, CreateBudgetDto>
{
    public async Task<CreateBudgetDto> Handle(CreateBudgetCommand request, CancellationToken canecllationToken)
    {
        var budget = Domain.BudgetAggregate.BudgetEntity.Budget.CreateBudget(
            request.Amount,
            request.Description,
            request.Duration,
            request.AccountId
        );
        var id = await budgetRepository.AddBudgetAsync(budget, canecllationToken);
        await unitOfWork.CommitChangesAsync(canecllationToken);
        return new CreateBudgetDto(id.Value);
    }
}
