using MediatR;
using Primitives.IntegrationEvent;

namespace BerryJuice.Infrastructure.EventBus;

internal class ExternalEventBus(
    IMediator mediator
) : IIntegrationEventPublisher
{
    private readonly IMediator _mediator = mediator;

    public async Task PublishAsync<TIntegrationEvent>(
        TIntegrationEvent @event,
        CancellationToken cancellationToken = default
    )
        where TIntegrationEvent : IIntegrationEvent
    {
        await _mediator.Publish(@event, cancellationToken);
    }
}
