using System.Data;

namespace BuildingBlocks.Infrastructure
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
