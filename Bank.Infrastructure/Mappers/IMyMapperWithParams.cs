namespace Bank.Infrastructure.Mappers
{
    public interface IMyMapperWithParams<in TEntity, out TViewModel> : IMapper where TEntity : class where TViewModel : class
    {
        TViewModel MapObject(TEntity entity, params object[] parameters);
    }
}