using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        bool RemoveAsync(TEntity item);
        bool RemoveAsync(int id);
        bool UpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(int id);
        IQueryable<TEntity> GetAll();
    }
}

