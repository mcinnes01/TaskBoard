using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TaskBoard.Extensions;
using TaskBoard.PostgreSql.Data.Contracts;

namespace TaskBoard.PostgreSql.Data
{
    public class NpgsqlConnectionFactory : IConnectionFactory
    {
        public DbConnection Create(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionString == null)
                throw new InvalidOperationException("No connection string named '{0}' could be found.".FormatWith(connectionStringName));
            return new NpgsqlConnection(connectionString.ConnectionString);

        }
    }
}