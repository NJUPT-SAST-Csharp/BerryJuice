namespace Primitives.IntegrationEvent;

public interface IIntegrationEventDispatcher
{
    public Task DispatchAsync(IIntegrationEvent @event);

    public void AddEventCallback<TEvent>(Func<TEvent, Task> callback)
        where TEvent : IIntegrationEvent;

    public void RemoveEventCallback<TEvent>()
        where TEvent : IIntegrationEvent;
}
