namespace Store.MVC.Services;

public interface IReadOnlyRepository<TEntity> where TEntity : IEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
}
