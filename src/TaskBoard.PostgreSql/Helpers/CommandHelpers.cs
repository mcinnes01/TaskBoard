using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace TaskBoard.PostgreSql.Helpers
{
    public static class CommandHelpers
    {
        /// <summary>
        /// Creates a NpgsqlCommand object with the given parameters.
        /// </summary>
        /// <param name="commandText">The PostgreSQL query to execute.</param>
        /// <param name="parameters">Parameters to pass to the PostgreSQL query.</param>
        /// <returns></returns>
        public static IDbCommand CreateCommand(IDbConnection connection, string commandText, Dictionary<string, object> parameters)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            AddParameters(command, parameters);

            return command;
        }

        /// <summary>
        /// Adds the parameters to a PostgreSQL command.
        /// </summary>
        /// <param name="commandText">The PostgreSQL query to execute.</param>
        /// <param name="parameters">Parameters to pass to the PostgreSQL query.</param>
        public static void AddParameters(IDbCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = param.Key;
                parameter.Value = param.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }
    }
}
