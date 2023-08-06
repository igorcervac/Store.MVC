using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Store.MVC.Tests;

public class ProductsControllerTests: IClassFixture<ServiceProviderFixture>
{
    private IMapper _mapper;
    private IReadOnlyQueryableRepository<Product> _productRepo;
    private ProductsController _controller;
    private ILogger<ProductsController> _logger;
    public ProductsControllerTests(ServiceProviderFixture fixture)
    {
        var serviceProvider = fixture.ServiceProvider;
        _mapper = serviceProvider.GetRequiredService<IMapper>();

        _productRepo = serviceProvider.GetRequiredService<IReadOnlyQueryableRepository<Product>>();
        _logger = serviceProvider.GetRequiredService<ILogger<ProductsController>>();
        _controller = new ProductsController(_productRepo, _mapper, _logger);
    }

    [Fact]
    public void Index_WhenProductTypeIdIsNull_ReturnsAllProducts()
    {
        // Arrange
        var products = _productRepo.GetAllAsync().GetAwaiter().GetResult();
        var expectedProductVms = _mapper.Map<List<ProductVm>>(products);
        
        // Act
        var result = _controller.Index(null).GetAwaiter().GetResult();

        // Assert
        Assert.NotNull(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        var actualProductVms = Assert.IsAssignableFrom<List<ProductVm>>(viewResult.ViewData.Model);
        // Serializing to compare as json because of nested productType
        var expectedProductVmsAsString = JsonSerializer.Serialize(expectedProductVms);
        var actualProductVmsAsString = JsonSerializer.Serialize(actualProductVms);
        Assert.Equal(expectedProductVmsAsString, actualProductVmsAsString);
    }

    [Fact]
    public void Index_WhenProductTypeIdIsNotNull_ReturnsOnlyProductsWithProductTypeId()
    {
        // Arrange
        const int productTypeId = 1;

        var products = _productRepo.GetAllAsync(p => p.ProductTypeId == productTypeId).GetAwaiter().GetResult();
        var expectedProductVms = _mapper.Map<List<ProductVm>>(products);

        // Act
        var result = _controller.Index(productTypeId).GetAwaiter().GetResult();

        // Assert
        Assert.NotNull(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        var actualProductVms = Assert.IsAssignableFrom<List<ProductVm>>(viewResult.ViewData.Model);
        // Serializing to compare as json because of nested productType
        var expectedProductVmsAsString = JsonSerializer.Serialize(expectedProductVms);
        var actualProductVmsAsString = JsonSerializer.Serialize(actualProductVms);
        Assert.Equal(expectedProductVmsAsString, actualProductVmsAsString);
    }

    [Fact]
    public void Details_WithValidId_ReturnsViewResult()
    {
        // Arrange
        var productId = _productRepo.GetAllAsync().GetAwaiter().GetResult()
            .Select(p => p.Id)
            .First();

        // Act
        var result = _controller.Details(productId).GetAwaiter().GetResult();

        // Assert
        Assert.NotNull(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        var product = Assert.IsAssignableFrom<ProductVm>(viewResult.ViewData.Model);
        Assert.NotNull(product);
        Assert.Equal(productId, product.Id);
    }

    [Fact]
    public void Details_WithInvalidId_ReturnNotFoundResult()
    {
        // Act
        var result = _controller.Details(-1).GetAwaiter().GetResult();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

}