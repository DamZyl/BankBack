using System.Threading.Tasks;

namespace Bank.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> Commit();

        void Rollback();
    }
}