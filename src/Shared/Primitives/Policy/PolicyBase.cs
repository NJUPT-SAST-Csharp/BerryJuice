using Primitives.EventBusScopedWrapper;

namespace Primitives.Policy;

public class PolicyBase(IEventBusWrapper eventBus)
{
    protected IEventBusWrapper _eventBus = eventBus;
}
