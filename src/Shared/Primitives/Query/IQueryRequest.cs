using MediatR;

namespace Primitives.Query;

public interface IQueryRequest<TResponse> : IRequest<TResponse> { }
