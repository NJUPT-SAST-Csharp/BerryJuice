using Primitives.Command;
using Primitives.Query;

namespace Primitives.EventBusScopedWrapper;

public interface IEventBusWrapper : IQueryRequestSender, ICommandRequestSender { }