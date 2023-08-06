using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Store.MVC.Tests;

public class OrderControllerTests: IClassFixture<ServiceProviderFixture>
{
    IReadOnlyQueryableRepository<Product> _productRepo;
    ICartItemRepository _cartItemRepo;
    IRepository<Order> _orderRepo;
    OrderController _controller;

    public OrderControllerTests(ServiceProviderFixture fixture)
    {
        var serviceProvider = fixture.ServiceProvider;

        _productRepo = serviceProvider.GetRequiredService<IReadOnlyQueryableRepository<Product>>();
        _cartItemRepo = serviceProvider.GetRequiredService<ICartItemRepository>();
        _orderRepo = serviceProvider.GetRequiredService<IRepository<Order>>();
        
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        var logger = serviceProvider.GetRequiredService<ILogger<OrderController>>();
        
        _controller = new OrderController(_orderRepo, _cartItemRepo, mapper, logger);
    }

    [Fact]
    public void Checkout_ReturnsViewResult()
    {
        // Act
        var result = _controller.Checkout();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Checkout_WhenModelStateIsInvalid_DoesNotAddOrderAndReturnsViewResult()
    {
        // Arrange
        var orderVm = new OrderVm();
        _controller.ModelState.AddModelError("First Name", "Please enter your first name");

        // Act
        var orderCountBefore = _orderRepo.GetAllAsync().GetAwaiter().GetResult().Count;
        var result = _controller.Checkout(orderVm).GetAwaiter().GetResult();
        var orderCountAfter = _orderRepo.GetAllAsync().GetAwaiter().GetResult().Count;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ViewResult>(result);
        Assert.Equal(orderCountBefore, orderCountAfter);
    }

    [Fact]
    public void Checkout_WhenModelStateIsValid_AddsOrderAndReturnsViewResult()
    {
        // Arrange
        var orderVm = new OrderVm {  FirstName = "First Name" };
        var productIds = _productRepo.GetAllAsync().GetAwaiter().GetResult()
            .Select(p => p.Id)
            .Take(2);
        
        // Act
        foreach (var productId in productIds)
        {
            _cartItemRepo.AddAsync(productId).GetAwaiter().GetResult();
        }
        var result = _controller.Checkout(orderVm).GetAwaiter().GetResult();

        // Assert
        Assert.NotNull(result);
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("CheckoutComplete", redirectToActionResult.ActionName);
        var orders = _orderRepo.GetAllAsync().GetAwaiter().GetResult();
        Assert.Equal(1, orders.Count());
        Assert.Equal(productIds.Count(), orders.First().OrderDetails.Count());
        Assert.Equal(0, _cartItemRepo.GetCount());
    }

    [Fact]
    public void CheckoutComplete_ReturnsViewResult()
    {
        // Act
        var result = _controller.CheckoutComplete();

        // Assert
        Assert.NotNull(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(OrderController.CheckoutCompleteMessage, viewResult.ViewData["CheckoutCompleteMessage"]);
    }
}
