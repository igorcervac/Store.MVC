using System.Linq.Expressions;
namespace Store.MVC.Services;

public interface IQueryableRepository<TEntity>
    where TEntity : IEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
}
