using Primitives.Command;
using Primitives.Query;

namespace BerryJuice.Blazor.EventBus;

public interface IEventBusWrapper : IQueryRequestSender, ICommandRequestSender { }
