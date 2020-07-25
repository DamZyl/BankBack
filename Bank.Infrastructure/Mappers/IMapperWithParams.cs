namespace Bank.Infrastructure.Mappers
{
    public interface IMapperWithParams<in TEntity, out TViewModel>
    {
        TViewModel MapObject(TEntity entity, params object[] parameters);
    }
}