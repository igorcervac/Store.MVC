using Store.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Store.MVC.Components
{
    public class CartSummary: ViewComponent
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartSummary(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public IViewComponentResult Invoke()
        {
            var cartVm = new CartVm { Count = _cartItemRepository.GetCount() };
            return View(cartVm);
        }
    }
}
