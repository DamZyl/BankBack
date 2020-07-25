using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly BankContext _bankContext;

        public GenericRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public async Task AddAsync(TEntity entity)
            => await _bankContext.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
            => await _bankContext.Set<TEntity>().AddRangeAsync(entities);

        public void Edit(TEntity entity)
            => _bankContext.Set<TEntity>().Update(entity);

        public async Task EditAsync(TEntity entity)
        {
            _bankContext.Set<TEntity>().Update(entity);
            await Task.CompletedTask;
        }

        public void EditRange(IEnumerable<TEntity> entities)
            => _bankContext.Set<TEntity>().UpdateRange(entities);

        public void Delete(TEntity entity)
            => _bankContext.Set<TEntity>().Remove(entity);

        public async Task DeleteAsync(TEntity entity)
        {
            _bankContext.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
            => _bankContext.Set<TEntity>().RemoveRange(entities);

        public async Task<TEntity> FindByIdAsync(Guid id)
            => await _bankContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate)
            => await _bankContext.Set<TEntity>().SingleOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
            => await _bankContext.Set<TEntity>().Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _bankContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity> FindByWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            IQueryable<TEntity> queryable = _bankContext.Set<TEntity>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }

            if (predicate == null)
            {
                return await queryable.FirstAsync();
            }

            return await queryable.SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAllWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            IQueryable<TEntity> queryable = _bankContext.Set<TEntity>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }

            return await queryable.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            IQueryable<TEntity> queryable = _bankContext.Set<TEntity>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }

            return await queryable.ToListAsync();
        }
    }
}