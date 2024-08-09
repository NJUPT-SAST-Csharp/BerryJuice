using MediatR;
using Messenger;

namespace BerryJuice.Infrastructure.EventBus;

internal class ExternalEventBus(IMediator mediator) : IMessagePublisher
{
    private readonly IMediator _mediator = mediator;

    public async Task<bool> PublishAsync<TMessage>(
        string channel,
        TMessage message,
        CancellationToken cancellationToken = default
    )
        where TMessage : IMessage
    {
        await _mediator.Publish(message, cancellationToken);
        return true;
    }
}
