using System.Data.Common;

namespace TaskBoard.PostgreSql.Data.Contracts
{
    public interface IConnectionFactory
    {
        DbConnection Create(string connectionStringName);
    }
}