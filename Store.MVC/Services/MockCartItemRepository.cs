namespace Store.MVC.Services;

public class MockCartItemRepository : ICartItemRepository
{
    private readonly IReadOnlyQueryableRepository<Product> _productRepository;
    public string CartId { get; set; }
    List<CartItem> _cartItems;

    public MockCartItemRepository(IReadOnlyQueryableRepository<Product> productRepository)
    {
        CartId = Guid.NewGuid().ToString();
        _cartItems = new();
        _productRepository = productRepository;
    }

    public static ICartItemRepository GetCart(IServiceProvider serviceProvider)
    {
        var productRepository = serviceProvider.GetService<IReadOnlyQueryableRepository<Product>>() ?? throw new Exception("Error initializing");
        var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
        var cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
        session?.SetString("CartId", cartId);
        return new MockCartItemRepository(productRepository) { CartId = cartId };
    }

    public async Task AddAsync(int productId)
    {
        var cartItem = _cartItems.FirstOrDefault(x => x.CartId == CartId && x.ProductId == productId);
        if (cartItem == null)
        {
            _cartItems.Add(new CartItem { ProductId = productId, Count = 1, CartId = CartId });
        }
        else
        {
            cartItem.Count++;
        }
        await Task.CompletedTask;
    }

    public async Task<IReadOnlyList<CartItem>> GetAllAsync()
    {
        var cartItems = _cartItems.Join(await _productRepository.GetAllAsync(), c=>c.ProductId, p=>p.Id, (c,p)=> 
        new CartItem
        {
            CartId = c.CartId,
            ProductId = c.ProductId,
            Product = p,
            Count = c.Count
        });
        return cartItems.ToList();
    }

    public int GetCount()
    {
        return _cartItems.Sum(c => c.Count);
    }

    public async Task<decimal> GetTotalAsync()
    {
        var cartItems = await GetAllAsync();
        return cartItems.Sum(c => c.Product.Price * c.Count);
    }

    public async Task RemoveAsync(int productId)
    {
        var cartItem = _cartItems.FirstOrDefault(c => c.CartId == CartId && c.ProductId == productId);
        if (cartItem != null)
        {
            if (cartItem.Count == 1)
            {
                _cartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Count--;
            }
        }
        await Task.CompletedTask;
    }

    public async Task ClearAsync()
    {
        _cartItems.Clear();
        await Task.CompletedTask;
    }
}
