using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Edit(TEntity entity);
        Task EditAsync(TEntity entity);
        void EditRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        
        Task<TEntity> FindByIdAsync(Guid id);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByWithIncludesAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes);
        Task<IEnumerable<TEntity>> FindAllWithIncludesAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes);
        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes);
    }
}