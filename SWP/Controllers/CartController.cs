using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Threading.Tasks;

namespace SWP.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IDiscountService _discountService;

        public CartController(ICartService cartService, IDiscountService discountService)
        {
            _cartService = cartService;
            _discountService = discountService;
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
            ViewBag.DiscountAmount = HttpContext.Session.GetDecimal("DiscountAmount") ?? 0;
            ViewBag.DiscountCode = HttpContext.Session.GetString("DiscountCode") ?? string.Empty;
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

        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(string discountCode)
        {
            var discount = await _discountService.GetDiscountAsync(discountCode);
            if (discount != null && discount.DiscountStatus)
            {
                HttpContext.Session.SetDecimal("DiscountAmount", discount.DiscountAmount);
                HttpContext.Session.SetString("DiscountCode", discountCode);
            }
            else
            {
                HttpContext.Session.SetDecimal("DiscountAmount", 0);
                HttpContext.Session.SetString("DiscountCode", string.Empty);
            }
            return RedirectToAction("Cart");
        }

    }

    public static class SessionExtensions
    {
        public static void SetDecimal(this ISession session, string key, decimal value)
        {
            session.SetString(key, value.ToString());
        }

        public static decimal? GetDecimal(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? (decimal?)null : decimal.Parse(value);
        }
    }
}
