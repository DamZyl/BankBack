using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Domain.Repositories;
using Bank.Infrastructure.Database;

namespace Bank.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankContext _bankContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        
        public UnitOfWork(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            IGenericRepository<TEntity> repository = new GenericRepository<TEntity>(_bankContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> Commit()
            => await _bankContext.SaveChangesAsync();
        

        public void Rollback()
            => _bankContext.ChangeTracker.Entries()
                .ToList()
                .ForEach(x => x.Reload());
        
    }
}