using Shared.Primitives.Query;

namespace Accounts.Application;

public sealed class TestQuery(int number) : IQueryRequest<int>
{
    public int Number { get; } = number;
}
