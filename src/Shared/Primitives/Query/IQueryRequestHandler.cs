using MediatR;

namespace Primitives.Query;

public interface IQueryRequestHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQueryRequest<TResult> { }
