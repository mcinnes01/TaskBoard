using System;
using System.Data;
using System.Threading.Tasks;
using TaskBoard.PostgreSql.Data.Contracts;

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
    }
}