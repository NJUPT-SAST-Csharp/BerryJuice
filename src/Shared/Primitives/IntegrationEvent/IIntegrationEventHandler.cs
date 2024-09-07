using MediatR;

namespace Primitives.IntegrationEvent;

internal interface IIntegrationEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : IIntegrationEvent
{ }