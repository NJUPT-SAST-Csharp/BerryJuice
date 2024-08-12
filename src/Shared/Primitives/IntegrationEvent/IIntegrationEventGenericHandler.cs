using MediatR;

namespace Primitives.IntegrationEvent;

public interface IIntegrationEventGenericHandler<in TNotification>
    : INotificationHandler<TNotification>
    where TNotification : class, IIntegrationEvent { }
