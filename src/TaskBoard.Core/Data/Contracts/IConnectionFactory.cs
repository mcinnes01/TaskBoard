using System.Data.Common;

namespace TaskBoard.Core.Data
{
    public interface IConnectionFactory
    {
        DbConnection Create(string connectionStringName);
    }
}