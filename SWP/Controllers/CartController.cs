using Microsoft.AspNetCore.Mvc;
using Service.Services.Cart;

namespace SWP.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Index", "Home");
            }

            await _cartService.AddToCartAsync(productId, userId, quantity);
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> Cart()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int orderDetailId)
        {
            await _cartService.RemoveFromCartAsync(orderDetailId);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int orderDetailId, int quantity)
        {
            if (quantity < 1)
            {
                return RedirectToAction("Cart");
            }

            await _cartService.UpdateQuantityAsync(orderDetailId, quantity);
            return RedirectToAction("Cart");
        }
    }
}
