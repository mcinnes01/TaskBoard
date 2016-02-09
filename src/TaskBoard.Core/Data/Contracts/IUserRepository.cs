using System;
using System.Threading.Tasks;
using TaskBoard.Core.Data.DTOs;
using TaskBoard.PostgreSql.Data.Contracts;

namespace TaskBoard.Core.Data.Contracts
{
    public interface IUserRepository : IRepository<UserRecord, Guid>
    {
        Task<UserRecord> GetUser(string username);
    }
}
