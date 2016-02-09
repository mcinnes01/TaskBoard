using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TaskBoard.Core.Data.Contracts;
using TaskBoard.Core.Data.DTOs;
using TaskBoard.Extensions;
using TaskBoard.PostgreSql.Data;
using TaskBoard.PostgreSql.Data.Contracts;

namespace TaskBoard.Core.Data
{
    public class UserRepository : RepositoryBase<UserRecord, Guid>, IUserRepository
    {
        public UserRepository(IConnectionFactory connectionFactory, string connectionStringName)
            : base(connectionFactory, connectionStringName) {}

        public async override Task<UserRecord> GetById(Guid id)
        {
            using (var conn = OpenConnection())
            {
                var sql = SQL.GetById.FormatWith("user");
                var result = await conn.QueryAsync<UserRecord>(sql, new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<UserRecord> GetUser(string username)
        {
            username = username.ToLower();

            using (var conn = OpenConnection())
            {
                var result = await conn.QueryAsync<UserRecord>(SQL.GetUser, new { username });
                return result.FirstOrDefault();
            }
        }
    }
}
