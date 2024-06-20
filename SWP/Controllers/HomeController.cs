using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service;

using Service.Service.ViewModels;
using Service.Services.Products;
using System.Diagnostics;


namespace SWP.Controllers
{
	public class HomeController : Controller
		
	{
		private readonly IProductService _productService;
		private readonly DiamondShopContext dbContext;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, DiamondShopContext dbContext, IProductService productService)
		{
			_logger = logger;
			this.dbContext = dbContext;
			_productService = productService ;
		}


		public async Task<IActionResult> Index()
		{
			var product = await _productService.GetProductsAsync();
			return View(product);
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Profile()
		{
			var userId = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrEmpty(userId))
			{
				return RedirectToAction("Login", "Account");
			}

			var user = dbContext.Users.Include(u => u.RoleID).FirstOrDefault(u => u.UserID.ToString() == userId);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}

			return View(user);
		}
	}
}
