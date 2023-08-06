using Microsoft.EntityFrameworkCore;

namespace Store.MVC.Services;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _context;

    public string CartId { get; set; } = string.Empty;

    private CartItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public static ICartItemRepository GetCart(IServiceProvider serviceProvider)
    {
        var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
        var cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
        session?.SetString("CartId", cartId);
        var context = serviceProvider.GetRequiredService<AppDbContext>() ?? throw new Exception("Error initializing");
        return new CartItemRepository(context) { CartId = cartId };
    }

    public async Task AddAsync(int productId)
    {
        var cartItem = await _context.CartItems
            .Where(x => x.CartId == CartId && x.ProductId == productId)
            .FirstOrDefaultAsync();

        if (cartItem == null)
        {
            _context.CartItems.Add(new CartItem { ProductId = productId, Count = 1, CartId = CartId });
        }
        else
        {
            cartItem.Count++;
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<CartItem>> GetAllAsync()
    {
        var cartItems = await _context.CartItems
            .AsNoTracking()
            .Include(c => c.Product)
            .Where(c => c.CartId == CartId)                
            .ToListAsync();
        return cartItems;
    }

    public int GetCount()
    {
        return _context.CartItems.Where(c => c.CartId == CartId).Sum(c => c.Count);
    }

    public async Task<decimal> GetTotalAsync()
    {
        // Sqlite doesn't support sum of decimal values, so we will use Linq to Objects to sum
        var cartItems = await _context.CartItems.Where(c => c.CartId == CartId)
            .Include(c => c.Product)
            .Select(c => c.Product.Price * c.Count)
            .ToListAsync();
        return cartItems.Sum();
    }

    public async Task RemoveAsync(int productId)
    {
        var cartItem = await _context.CartItems.Where(c => c.CartId == CartId && c.ProductId == productId).FirstOrDefaultAsync();
        if (cartItem != null)
        {
            if (cartItem.Count == 1)
            {
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Count--;
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task ClearAsync()
    {
        var cartItems = _context.CartItems.Where(c => c.CartId == CartId);
        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
    }
}
