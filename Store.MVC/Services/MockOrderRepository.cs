namespace Store.MVC.Services;

public class MockOrderRepository : IRepository<Order>
{
    private readonly ICartItemRepository _cartItemRepository;
    private List<Order> _orders;
    
    public MockOrderRepository(ICartItemRepository cartItemRepository) 
    { 
        _orders = new List<Order>();
        _cartItemRepository = cartItemRepository;
    }
    
    public async Task<IReadOnlyList<Order>> GetAllAsync()
    {
        return await Task.FromResult(_orders);
    }

    public async Task AddAsync(Order order)
    {
        order.OrderDetails = new List<OrderDetail>();
        foreach (var cartItem in await _cartItemRepository.GetAllAsync())
        {
            var orderDetail = new OrderDetail
            {
                ProductId = cartItem.ProductId,
                Count = cartItem.Count,
                Price = cartItem.Product.Price
            };
            order.OrderDetails.Add(orderDetail);
        }
        _orders.Add(order);
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
    }
}
