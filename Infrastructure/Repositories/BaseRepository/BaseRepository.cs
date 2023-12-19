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

        public BaseRepository(ApplicationContext entity)
        {
            _entity = entity.Set<TEntity>();
            _context = entity;
        }


        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                await _entity.AddAsync(entity);

                _context.SaveChanges();

                return true;
            }
            catch
            {

                return false;
            }
        }
        public bool RemoveAsync(TEntity item)
        {
            try
            {
                _context.Remove(item);

                _context.SaveChanges();

                return true;
            }

            catch
            {
                return false; 
            }
        }
        public bool UpdateAsync(TEntity entity)
        {
            try {
                _entity.Update(entity);

                _context.SaveChanges();

                return true;
            }
            catch
            {

                return false;
            }
        }
        public async Task<TEntity> GetAsync(int id)
        {
            var result = await _entity.FindAsync(id);

            return result;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _entity.AsNoTracking().ToListAsync();

            return result;
        }
        public async Task<IEnumerable<TEntity>> GetAllWhereAsync(Expression<Func<TEntity,bool>> filter)
        {
            var result = _entity.Where(filter) ;

            return result;
        }
    }
}
