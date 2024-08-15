using Shared.Primitives.Query;

namespace Accounts.Application.TestService.GetTest;

public sealed class TestQuery() : IQueryRequest<int>
{
    public int Number { get; } = Random.Shared.Next();
}
