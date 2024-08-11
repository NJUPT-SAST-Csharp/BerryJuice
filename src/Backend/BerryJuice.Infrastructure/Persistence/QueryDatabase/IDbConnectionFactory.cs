using System.Data;

namespace BerryJuice.Infrastructure.Persistence.QueryDatabase;

internal interface IDbConnectionFactory : IDisposable
{
    public IDbConnection GetConnection();
}
