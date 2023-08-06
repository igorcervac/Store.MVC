namespace Store.MVC.Services;

public interface IWriteableRepository<TEntity> where TEntity: IEntity
{
    Task AddAsync(TEntity enitity);
}
