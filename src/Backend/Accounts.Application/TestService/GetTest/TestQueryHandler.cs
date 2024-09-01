using Shared.Primitives.Query;

namespace Accounts.Application.TestService.GetTest;

internal sealed class TestQueryHandler : IQueryRequestHandler<TestQuery, int>
{
    public async Task<int> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(2000, cancellationToken);
        return request.Number;
    }
}
