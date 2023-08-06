using Microsoft.EntityFrameworkCore;

namespace Store.MVC.Services;

public class OrderRepository : IRepository<Order>
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Order>> GetAllAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();
    }
}
