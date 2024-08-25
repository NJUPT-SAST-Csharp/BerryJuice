using Microsoft.Extensions.DependencyInjection;
using Primitives.Command;
using Primitives.Query;
using Shared.Primitives.Query;

namespace Primitives.EventBusScopedWrapper;

public class EventBusWrapper(IServiceScopeFactory scopeFactory) : IEventBusWrapper
{
    public async Task<TResponse> CommandAsync<TResponse>(
        ICommandRequest<TResponse> command,
        CancellationToken cancellationToken = default
    )
    {
        return await DoInScope(async serviceProvider =>
        {
            var commandSender = serviceProvider.GetRequiredService<ICommandRequestSender>();
            return await commandSender.CommandAsync(command, cancellationToken);
        });
    }

    public async Task CommandAsync(
        ICommandRequest command,
        CancellationToken cancellationToken = default
    )
    {
        await DoInScope(async serviceProvider =>
        {
            var commandSender = serviceProvider.GetRequiredService<ICommandRequestSender>();
            await commandSender.CommandAsync(command, cancellationToken);
        });
    }

    public async Task<TResponse> QueryAsync<TResponse>(
        IQueryRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        return await DoInScope(async serviceProvider =>
        {
            var query = serviceProvider.GetRequiredService<IQueryRequestSender>();
            return await query.QueryAsync(request, cancellationToken);
        });
    }

    private async Task DoInScope(Func<IServiceProvider, Task> action)
    {
        using var scope = scopeFactory.CreateScope();
        await action.Invoke(scope.ServiceProvider);
    }

    private async Task<TResponse> DoInScope<TResponse>(
        Func<IServiceProvider, Task<TResponse>> action
    )
    {
        using var scope = scopeFactory.CreateScope();
        return await action.Invoke(scope.ServiceProvider);
    }
}
