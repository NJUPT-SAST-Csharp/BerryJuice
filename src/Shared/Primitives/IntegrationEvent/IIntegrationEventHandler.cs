using MediatR;

namespace Primitives.IntegrationEvent;

public interface IIntegrationEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : IIntegrationEvent { }
