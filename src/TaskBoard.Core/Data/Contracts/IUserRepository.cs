using System;
using System.Threading.Tasks;
using TaskBoard.Core.Data.DTOs;

namespace TaskBoard.Core.Data.Contracts
{
    public interface IUserRepository : IRepository<UserRecord, Guid>
    {
        Task<UserRecord> GetUser(string username);
    }
}
