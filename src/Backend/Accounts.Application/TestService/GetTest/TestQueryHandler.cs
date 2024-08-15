using Shared.Primitives.Query;

namespace Accounts.Application.TestService.GetTest;

internal sealed class TestQueryHandler : IQueryRequestHandler<TestQuery, int>
{
    public Task<int> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(request.Number);
    }
}
