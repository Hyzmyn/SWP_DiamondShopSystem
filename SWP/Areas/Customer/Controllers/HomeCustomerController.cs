using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Services;
using SWP.Controllers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SWP.Areas.Customer.Controllers
{
	[Area("customer")]
	[Route("customer")]
	[Route("customer/homecustomer")]
	public class HomeCustomerController : Controller
	{
		private readonly IUserService _userService;
		private readonly ICartService _cartService;
		private readonly IDiscountService _discountService;
        private readonly IOrderService _orderService;
        private readonly IWarrantyService _warrantyService;
        private readonly DiamondShopContext _context;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly DiamondShopContext _dbContext;
        public HomeCustomerController(IUserService userService, ICartService cartService, IDiscountService discountService, IOrderService orderService, IWarrantyService warrantyService, DiamondShopContext context, ICompositeViewEngine viewEngine, IServiceProvider serviceProvider, DiamondShopContext dbContext)
        {
            _userService = userService;
            _cartService = cartService;
			_discountService = discountService;
            _orderService = orderService;
            _warrantyService = warrantyService;
            _context = context;
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;

        }

        [Route("")]
		[Route("customer")]
		public async Task<IActionResult> Index()
		{
			var userId = HttpContext.Session.GetString("UserId");
			if (userId == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await _userService.GetUserByIdAsync(int.Parse(userId));
			return View(user);
		}

		[Route("edit")]
		public async Task<IActionResult> Edit(string field)
		{
			var userId = HttpContext.Session.GetString("UserId");
			if (userId == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await _userService.GetUserByIdAsync(int.Parse(userId));
			if (user == null)
			{
				return NotFound();
			}

			ViewBag.Field = field;
			return View(user);
		}

		[HttpPost]
		[Route("edit")]
		public async Task<IActionResult> Edit(string field, string newValue)
		{
			var userId = HttpContext.Session.GetString("UserId");
			if (userId == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await _userService.GetUserByIdAsync(int.Parse(userId));
			if (user == null)
			{
				return NotFound();
			}

			switch (field.ToLower())
			{
				case "username":
					user.Username = newValue;
					break;
				case "email":
					user.Email = newValue;
					break;
				case "phonenumber":
					user.PhoneNumber = newValue;
					break;
				case "address":
					user.Address = newValue;
					break;
				case "nisize":
					user.NiSize = newValue;
					break;
				default:
					return BadRequest("Invalid field");
			}
			HttpContext.Session.SetString("Username", user.Username);
			HttpContext.Session.SetString("Email", user.Email);
			HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
			HttpContext.Session.SetString("Address", user.Address);
			HttpContext.Session.SetString("NiSize", user.NiSize);

			await _userService.UpdateUserAsync(user);
            return RedirectToAction("Index");
		}
        [HttpPost]
        [Route("updatequantity")]
        public async Task<IActionResult> UpdateQuantity(int orderDetailId, int quantity)
        {
            if (quantity < 1)
            {
                return RedirectToAction("OrderOfCustomer");
            }

            await _cartService.UpdateQuantityAsync(orderDetailId, quantity);
            return RedirectToAction("OrderOfCustomer");
        }

        [HttpPost]
        [Route("removefromcart")]
        public async Task<IActionResult> RemoveFromCart(int orderDetailId)
        {
            await _cartService.RemoveFromCartAsync(orderDetailId);
            return RedirectToAction("OrderOfCustomer");
        }

        [HttpPost]
        [Route("applydiscount")]
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
            return RedirectToAction("OrderOfCustomer");
        }

        [HttpPost]
        [Route("checkout")]
        public IActionResult Checkout()
        {
            // Thêm logic checkout ở đây nếu cần thiết
            return RedirectToAction("OrderOfCustomer");
        }

        [Route("orderofcustomer")]
        public async Task<IActionResult> OrderOfCustomer()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var cartItems = await _cartService.GetCartItemsAsync(int.Parse(userId));
            ViewBag.DiscountAmount = HttpContext.Session.GetDecimal("DiscountAmount") ?? 0;
            ViewBag.DiscountCode = HttpContext.Session.GetString("DiscountCode") ?? string.Empty;
            return View(cartItems);
        }

        [Route("customerinformation")]
		public IActionResult CustomerInformation()
		{
			return View();
		}

        [Route("historyofcustomer")]
        public async Task<IActionResult> HistoryOfCustomer()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var orders = await _orderService.GetOrdersByUserIdAsync(int.Parse(userId));
            return View(orders);
		}
        [HttpGet]
        [Route("changepassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int parsedUserId;
            if (!int.TryParse(userId, out parsedUserId))
            {
                ModelState.AddModelError(string.Empty, "Invalid user ID.");
                return View();
            }

            if (string.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "Current password is required.");
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("newPassword", "Please add new password");
            }
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "The new password and confirmation password do not match.");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isCurrentPasswordValid = await _userService.VerifyPasswordAsync(parsedUserId, currentPassword);
            if (!isCurrentPasswordValid)
            {
                ModelState.AddModelError("currentPassword", "Current password is incorrect.");
                return View();
            }

            bool isPasswordChanged = await _userService.ChangePasswordAsync(parsedUserId, newPassword);
            if (isPasswordChanged)
            {
                ViewBag.Message = "Password has been changed successfully.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to change the password.");
            }

            return View();
        }



        [Route("orderdetails")]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null || order.UserID != int.Parse(userId))
            {
                return NotFound();
            }

            return View(order);
        }
        [Route("warranties")]
        public async Task<IActionResult> Warranties()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }
            var warranties = await _context.Warranties
                .Include(w => w.Order)
                .Include(w => w.Product)
                .Where(w => w.Order.UserID == int.Parse(userId) && w.Order.OrderStatus == true)
                .ToListAsync();
            return View(warranties);
        }

        [Route("warranty-detail/{id}")]
        public async Task<IActionResult> WarrantyDetails(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }
            var warranty = await _context.Warranties
                .Include(w => w.Order)
                    .ThenInclude(o => o.User)
                .Include(w => w.Product)
                    .ThenInclude(p => p.Gems)
                .FirstOrDefaultAsync(w => w.WarrantyID == id && w.Order.UserID == int.Parse(userId) && w.Order.OrderStatus == true);
            if (warranty == null)
            {
                return NotFound();
            }
            return View(warranty);
        }

    }
}
