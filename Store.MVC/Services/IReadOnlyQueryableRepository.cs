namespace Store.MVC.Services;

public interface IReadOnlyQueryableRepository<TEntity>: IReadOnlyRepository<TEntity>, IQueryableRepository<TEntity>
    where TEntity : IEntity
{
}
