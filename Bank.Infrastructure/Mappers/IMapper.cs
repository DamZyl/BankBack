namespace Bank.Infrastructure.Mappers
{
    public interface IMapper<in TEntity, out TViewModel>
    {
        TViewModel MapObject(TEntity entity);
    }
}