using AutoMapper;
using Store.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Store.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartItemRepository cartItemRepository, IMapper mapper, ILogger<CartController> logger)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Title = "Cart";

                var cartVm = new CartVm { TotalCost = await _cartItemRepository.GetTotalAsync(), Count = _cartItemRepository.GetCount() };
                var cartItems = await _cartItemRepository.GetAllAsync();
                cartVm.Items = _mapper.Map<List<CartItemVm>>(cartItems);

                return View(cartVm);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while fetching cart items", ex);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> Add(int productId) 
        {
            try
            {
                await _cartItemRepository.AddAsync(productId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while adding product with id {productId} to the cart", ex);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> Remove(int productId) 
        {
            try
            {
                await _cartItemRepository.RemoveAsync(productId);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while removing product with id {productId} from the cart", ex);
                return StatusCode(500);
            }

        }
    }
}
