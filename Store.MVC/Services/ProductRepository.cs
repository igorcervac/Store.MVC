using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Store.MVC.Services;

public class ProductRepository : IReadOnlyQueryableRepository<Product>
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .Include(p => p.ProductType)
            .OrderBy(p => p.Id)
            .ToListAsync();
        return products;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(Expression<Func<Product, bool>> expression)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Include(p => p.ProductType)
            .Where(expression)
            .OrderBy(p => p.Id)
            .ToListAsync();
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.ProductType)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        return product;
    }
}
