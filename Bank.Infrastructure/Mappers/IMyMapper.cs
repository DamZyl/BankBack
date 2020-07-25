namespace Bank.Infrastructure.Mappers
{
    public interface IMyMapper<in TEntity, out TViewModel> : IMapper where TEntity : class where TViewModel : class
    {
        TViewModel MapObject(TEntity entity);
    }
}