using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Store.MVC.Tests;

public class CartControllerTests: IClassFixture<ServiceProviderFixture>
{
    IReadOnlyQueryableRepository<Product> _productRepo;
    ICartItemRepository _cartItemRepo;
    readonly CartController _controller;

    public CartControllerTests(ServiceProviderFixture fixture) 
    {
        var serviceProvider = fixture.ServiceProvider;
        _productRepo = serviceProvider.GetRequiredService<IReadOnlyQueryableRepository<Product>>();
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        _cartItemRepo = serviceProvider.GetRequiredService<ICartItemRepository>();
        var logger = serviceProvider.GetRequiredService<ILogger<CartController>>();
        
        _controller = new CartController(_cartItemRepo, mapper, logger);
    }

    [Fact]
    public void Index_ReturnsCartVm()
    {
        // Act
        var result = _controller.Index().GetAwaiter().GetResult();

        // Assert
        Assert.NotNull(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<CartVm>(viewResult.ViewData.Model);
    }

    [Fact]
    public void Add_WithProductId_AddsCartItemWithProductId()
    {
        // Arrange
        var productId = _productRepo.GetAllAsync().GetAwaiter().GetResult()
            .Select(p => p.Id)
            .First();

        // Act
        var cartItemCountBefore = _cartItemRepo.GetCount();
        var result = _controller.Add(productId).GetAwaiter().GetResult();
        var cartItemCountAfter = _cartItemRepo.GetCount();

        // Assert
        Assert.NotNull(result);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal(cartItemCountBefore+1, cartItemCountAfter);
    }

    [Fact]
    public void Remove_WithProductIdAndTwoCartItems_RemovesCartItemWithProductId()
    {
        // Arrange
        var productId = _productRepo.GetAllAsync().GetAwaiter().GetResult()
            .Select(p => p.Id)
            .First();        
        _controller.Add(productId).GetAwaiter().GetResult();

        // Act
        var cartItemCountBefore = _cartItemRepo.GetCount();
        var result = _controller.Remove(productId).GetAwaiter().GetResult();
        var cartItemCountAfter = _cartItemRepo.GetCount();

        // Assert
        Assert.NotNull(result);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal(cartItemCountBefore-1, cartItemCountAfter);
    }
}
