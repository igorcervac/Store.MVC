namespace Store.MVC.Services;

public interface IRepository<T>: IReadOnlyRepository<T>, IWriteableRepository<T>
    where T:IEntity
{
}
