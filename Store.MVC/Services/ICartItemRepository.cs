namespace Store.MVC.Services;

public interface ICartItemRepository
{
    Task ClearAsync();
    Task<IReadOnlyList<CartItem>> GetAllAsync();
    Task AddAsync(int productId);
    Task RemoveAsync(int productId);
    Task<decimal> GetTotalAsync();
    int GetCount();
}
