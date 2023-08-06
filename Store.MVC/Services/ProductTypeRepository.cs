using Microsoft.EntityFrameworkCore;
namespace Store.MVC.Services;

public class ProductTypeRepository : IReadOnlyRepository<ProductType>
{
    private readonly AppDbContext _context;

    public ProductTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProductType>> GetAllAsync()
    {
        var productTypes = await _context.ProductTypes
            .AsNoTracking()
            .ToListAsync();
        return productTypes;
    }

    public async Task<ProductType?> GetByIdAsync(int id)
    {
        var productType = await _context.ProductTypes
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        return productType;
    }
}
