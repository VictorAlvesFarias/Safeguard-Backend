
using Domain.Entitites;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.BaseRepository
{
    //Definição dos tipos do repositorio generico, o parametro "where", define que ela deve ser do tipo TEntity
    //E o tipo TEntity deve ser uma classe, observe no arquivo de Ioc
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entity;
        private readonly ApplicationContext _context;

        public BaseRepository(
            ApplicationContext entity
        )
        {
            _entity = entity.Set<TEntity>();
            _context = entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity is BaseEntityUserRelation )
            {
                var baseEntity = entity as BaseEntityUserRelation;
                
                if (baseEntity is not null)
                {
                    baseEntity.SetUser(_context.GetUserId());    
                }
            }

            var result = await _entity.AddAsync(entity);

            _context.SaveChanges();

            return result.Entity;
        }
        public bool RemoveAsync(TEntity item)
        {
            _context.Remove(item);

            _context.SaveChanges();

            return true;
        }
        public bool RemoveAsync(int id)
        {
            _context.Remove (_entity.FindAsync(id).Result);
            _context.SaveChanges();

            return true;
        }
        public bool UpdateAsync(TEntity entity)
        {
            _entity.Update(entity);

            _context.SaveChanges();

            return true;
        }
        public async Task<TEntity> GetAsync(int id)
        {
            var item = await _entity.FindAsync(id);

            return item;
        }
        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _entity;

            if (typeof(BaseEntityUserRelation).IsAssignableFrom(typeof(TEntity)))
            {
                var userId = _context.GetUserId();
                var newQuery = query.OfType<BaseEntityUserRelation>() .Where(x => x.UserId == userId).Cast<TEntity>();
                return newQuery;
            }

            return query;
        }

    }
}