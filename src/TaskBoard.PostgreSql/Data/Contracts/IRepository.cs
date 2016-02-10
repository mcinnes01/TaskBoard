﻿using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace TaskBoard.PostgreSql.Data.Contracts
{
    public interface IRepository<TEntity, in TId> where TEntity : class, new()
    {
        Task<TEntity> GetById(TId id);
    }
}