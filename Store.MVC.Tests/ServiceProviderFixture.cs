using Microsoft.Extensions.DependencyInjection;

namespace Store.MVC.Tests;

public class ServiceProviderFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; set; }

    public ServiceProviderFixture() 
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IRepository<Order>,OrderRepository>();
        serviceCollection.AddScoped<IReadOnlyRepository<ProductType>, MockProductTypeRepository>();
        serviceCollection.AddScoped<IReadOnlyQueryableRepository<Product>, MockProductRepository>();
        serviceCollection.AddScoped<ICartItemRepository, MockCartItemRepository>();
        serviceCollection.AddScoped<IRepository<Order>, MockOrderRepository>();

        var configuration = new MapperConfiguration(x => x.AddMaps(new[] { "Store.MVC" }));
        var mapper = configuration.CreateMapper();
        mapper = configuration.CreateMapper();
        serviceCollection.AddScoped( _ => mapper);

        serviceCollection.AddLogging();
        
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public void Dispose()
    {
        ServiceProvider.Dispose();
    }
}
