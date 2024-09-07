namespace Primitives.Query;

public interface IQueryRequestSender
{
    public Task<TResponse> QueryAsync<TResponse>(
        IQueryRequest<TResponse> request,
        CancellationToken cancellationToken = default
    );
}
