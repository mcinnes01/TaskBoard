using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace TaskBoard.Core.Data
{
    public class NpgsqlConnectionFactory : IConnectionFactory
    {
        public DbConnection Create(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionString == null)
                throw new InvalidOperationException("No connection string named '{0}' could be found.", connectionString.FormatWith(connectionStringName));
            return new NpgsqlConnection(connectionString.ConnectionString);
        }
    }
}