using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TaskBoard.PostgreSql.Data.Contracts;
using TaskBoard.PostgreSql.Helpers;

namespace TaskBoard.PostgreSql.Data
{
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, new()
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _connectionStringName;

        public RepositoryBase(IConnectionFactory connectionFactory, string connectionStringName)
        {
            if (connectionFactory == null) throw new ArgumentNullException("connectionFactory");
            if (string.IsNullOrEmpty(connectionStringName)) throw new ArgumentNullException("connectionStringName");

            _connectionFactory = connectionFactory;
            _connectionStringName = connectionStringName;
        }

        public IDbConnection OpenConnection()
        {
            var connection = _connectionFactory.Create(_connectionStringName);
            connection.Open();
            return connection;
        }

        public abstract Task<TEntity> GetById(TId id);

        /// <summary>
        /// Executes a non-query PostgreSQL statement.
        /// </summary>
        /// <param name="commandText">The PostgreSQL query to execute.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>
        /// <returns>The count of records affected by the PostgreSQL statement.</returns>
        public int Execute(string commandText, Dictionary<string, object> parameters)
        {
            int result = 0;

            if (String.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            using (var conn = OpenConnection())
            {
                var command = CommandHelpers.CreateCommand(conn, commandText, parameters);
                result = command.ExecuteNonQuery();
            }

            return result;
        }

        /// <summary>
        /// Executes a PostgreSQL query that returns a single scalar value as the result.
        /// </summary>
        /// <param name="commandText">The PostgreSQL query to execute.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>
        /// <returns></returns>
        public object QueryValue(string commandText, Dictionary<string, object> parameters)
        {
            object result = null;

            if (String.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            using (var conn = OpenConnection())
            {
                var command = CommandHelpers.CreateCommand(conn, commandText, parameters);
                result = command.ExecuteScalar();
            }

            return result;
        }

        /// <summary>
        /// Helper method to return query a string value. 
        /// </summary>
        /// <param name="commandText">The PostgreSQL query to execute.</param>
        /// <param name="parameters">Parameters to pass to the PostgreSQL query.</param>
        /// <returns>The string value resulting from the query.</returns>
        public string GetStrValue(string commandText, Dictionary<string, object> parameters)
        {
            string value = QueryValue(commandText, parameters) as string;
            return value;
        }

        /// <summary>
        /// Executes a SQL query that returns a list of rows as the result.
        /// </summary>
        /// <param name="commandText">The PostgreSQL query to execute.</param>
        /// <param name="parameters">Parameters to pass to the PostgreSQL query.</param>
        /// <returns>A list of a Dictionary of Key, values pairs representing the ColumnName and corresponding value.</returns>
        public List<Dictionary<string, string>> Query(string commandText, Dictionary<string, object> parameters)
        {
            List<Dictionary<string, string>> rows = null;
            if (String.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }
            
            using (var conn = OpenConnection())
            {
                var command = CommandHelpers.CreateCommand(conn, commandText, parameters);
                using (IDataReader reader = command.ExecuteReader())
                {
                    rows = new List<Dictionary<string, string>>();
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, string>();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            var columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i).ToString();
                            row.Add(columnName, columnValue);
                        }
                        rows.Add(row);
                    }
                }
            }

            return rows;
        }
    }
}