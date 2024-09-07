using System.Data;

namespace Primitives.QueryDatabase;

public interface IDbConnectionFactory : IDisposable
{
    public IDbConnection GetConnection();
}