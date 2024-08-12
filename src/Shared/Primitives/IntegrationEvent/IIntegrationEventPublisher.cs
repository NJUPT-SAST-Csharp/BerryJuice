namespace Primitives.IntegrationEvent;

internal interface IIntegrationEventPublisher
{
    public Task PublishAsync<TEvent>(
        TEvent domainEvent,
        CancellationToken cancellationToken = default
    )
        where TEvent : IIntegrationEvent;
}
