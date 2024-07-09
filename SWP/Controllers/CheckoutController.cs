using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Services;
using Service.Services.VNPay;
using Service.ViewModel;
using SWP.Helper;
using System.Threading.Tasks;

namespace SWP.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly ICartService _cartService;
		private readonly IDiscountService _discountService;
		private readonly DiamondShopContext _dbContext;
		private readonly IVnPayService _vnPayService;
		private readonly IWalletService _walletService;
		private readonly ICompositeViewEngine _viewEngine;
		private readonly IServiceProvider _serviceProvider;
		private readonly IWarrantyService _warrantyService;

		public CheckoutController(ICartService cartService, IDiscountService discountService, DiamondShopContext dbContext, IVnPayService vnPayService, IWalletService walletService, ICompositeViewEngine viewEngine, IServiceProvider serviceProvider, IWarrantyService warrantyService)
		{
			_cartService = cartService;
			_discountService = discountService;
			_dbContext = dbContext;
			_vnPayService = vnPayService;
			_walletService = walletService;
			_viewEngine = viewEngine;
			_serviceProvider = serviceProvider;
			_warrantyService = warrantyService;
		}

		public async Task<IActionResult> IndexAsync(decimal totalPrice, int orderId)
		{
			await _cartService.UpdateOrder(orderId, totalPrice);

            if (HttpContext.Session.GetString("UserId") == null)
			{
				return RedirectToAction("Index", "Home");
			}

			if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
			if (user == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var cartItems = await _cartService.GetCartItemsAsync(userId);
			var userOrder = new UserOrderVM
			{
				User = user,
				OrderDetail = cartItems
			};

			ViewBag.DiscountAmount = HttpContext.Session.GetDecimal("DiscountAmount") ?? 0;
			ViewBag.DiscountCode = HttpContext.Session.GetString("DiscountCode") ?? string.Empty;

			return View(userOrder);
		}

		[HttpPost]
		public async Task<IActionResult> CheckoutAsync([FromForm] string flexRadioDefault)
		{

            if (HttpContext.Session.GetString("UserId") == null)
			{
				return RedirectToAction("Index", "Home");
			}

			if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
			if (user == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.UserID == userId && !o.OrderStatus);
			if (order == null)
			{
				return RedirectToAction("Index", "Home");
			}
            


            if (flexRadioDefault == "paypal")
			{
				var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, order);
				return Redirect(paymentUrl);
			}
			else if (flexRadioDefault == "cashonDelivery")
			{
				// Handle Cash on Delivery payment
			}

			return View();
		}


		public async Task<IActionResult> PaymentRollBack()
		{
			var response = _vnPayService.PaymentExecute(Request.Query);
			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = "Lỗi Thanh Toán!";
				return RedirectToAction("Cart", "Cart");
			}


			return RedirectToAction("PaymentSuccess", new { amount = response.Amount });

		}

		public async Task<IActionResult> PaymentSuccess(int amount)
		{
			if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
			{
				return RedirectToAction("Index", "Home");
			}

			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.UserID == userId && !o.OrderStatus);
			if (order == null)
			{
				return RedirectToAction("Index", "Home");
			}
			await PointUserAsync(amount);
			await _warrantyService.AddWarrantyAsync(userId);

			var model = _dbContext.Warranties.Include(o => o.Order).ThenInclude(o => o.User).Include(o => o.Product).ThenInclude(o => o.Gems).Where(p => p.OrderID == order.OrderID).ToList();
			if (model != null)
			{
				return View(model);
			}
			return RedirectToAction("Index", "Home");
		}

		public async Task PointUserAsync(int amount)
		{
			if (int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
			{
				var points = amount / 100000;
				var wallet = await _walletService.GetWalletPointByUserIdAsync(userId);
                string discountCode = HttpContext.Session.GetString("DiscountCode");
                await _discountService.SubtractUserPointAsync(discountCode, userId);
                HttpContext.Session.Remove("DiscountCode");
                if (wallet != null)
				{
					await _walletService.UpdatWalletPointAsync(userId, points);
				}
				else
				{
					await _walletService.AddWalletPointAsync(userId, points);
				}
			}
		}
	}
}
