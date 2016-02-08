using System.Threading.Tasks;

namespace TaskBoard.Core.Data.Contracts
{
    public interface IRepository<TEntity, in TId> where TEntity : class, new()
    {
        Task<TEntity> GetById(TId id);
    }
}