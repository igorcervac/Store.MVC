namespace Store.MVC.Services;

public class MockProductTypeRepository : IReadOnlyRepository<ProductType>
{
    private IReadOnlyList<ProductType> _productTypes;

    public MockProductTypeRepository()
    {
        _productTypes = new List<ProductType>()
        {
            new () { Id = 1, Name = "Amber" },
            new () { Id = 2, Name = "Dark" },
            new () { Id = 3, Name = "Clear" }
        };

    }
    public Task<IReadOnlyList<ProductType>> GetAllAsync()
    {
        return Task.FromResult(_productTypes);
    }

    public Task<ProductType?> GetByIdAsync(int id)
    {
        return Task.FromResult(_productTypes.Where(p => p.Id == id).FirstOrDefault());
    }
}
