using System;
using System.Data;
using System.Threading.Tasks;
using TaskBoard.Core.Data.Contracts;

namespace TaskBoard.Core.Data
{
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, new()
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _connectionStringName;

        protected RepositoryBase(IConnectionFactory connectionFactory, string connectionStringName)
        {
            if (connectionFactory == null) throw new ArgumentNullException("connectionFactory");
            if (string.IsNullOrEmpty(connectionStringName)) throw new ArgumentNullException("connectionStringName");

            _connectionFactory = connectionFactory;
            _connectionStringName = connectionStringName;
        }

        protected IDbConnection OpenConnection()
        {
            var connection = _connectionFactory.Create(_connectionStringName);
            connection.Open();
            return connection;
        }

        public abstract Task<TEntity> GetById(TId id);
    }
}