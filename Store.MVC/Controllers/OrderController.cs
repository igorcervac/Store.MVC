using Store.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Store.MVC.Controllers;

public class OrderController : Controller
{
    private readonly IRepository<Order> _orderRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderController> _logger;

    public static string CheckoutCompleteMessage => "Order complete! Thank you for your order!";

    public OrderController(IRepository<Order> orderRepository, ICartItemRepository cartItemRepository, IMapper mapper, ILogger<OrderController> logger)
    {
        _orderRepository = orderRepository;
        _cartItemRepository = cartItemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public IActionResult Checkout()
    {
        ViewBag.Title = "Checkout";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(OrderVm orderVm) 
    {
        if (ModelState.IsValid)
        {
            try
            {
                var order = _mapper.Map<Order>(orderVm);
                order.Date = DateTime.Now;
                order.Total = await _cartItemRepository.GetTotalAsync();

                var cartItems = await _cartItemRepository.GetAllAsync();
                order.OrderDetails = _mapper.Map<List<OrderDetail>>(cartItems);

                await _orderRepository.AddAsync(order);

                await _cartItemRepository.ClearAsync();

                return RedirectToAction("CheckoutComplete");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while adding order", ex);
                return StatusCode(500);
            }
        }
        return View();
    }

    public IActionResult CheckoutComplete()
    {
        ViewBag.CheckoutCompleteMessage = CheckoutCompleteMessage;
        return View();
    }
}
