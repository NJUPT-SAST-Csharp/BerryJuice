using Shared.Primitives.Query;

namespace Accounts.Application;

internal sealed class TestQueryHandler : IQueryRequestHandler<TestQuery, int>
{
    public async Task<int> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        return request.Number;
    }
}
