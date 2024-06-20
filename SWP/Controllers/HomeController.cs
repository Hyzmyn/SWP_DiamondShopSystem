using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Repository.Models;
using Repository.Repositories.Base;
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
			_productService = productService;

		}


		public IActionResult Index()
		{
			var product = _productService.GetProducts();
			return View(product);
		}
		[Route("DiamondJewelery")]
		public async Task<IActionResult> DiamondJewelery(string keyword, int pageNumber = 1, int pageSize = 10)
		{
			var products = await _productService.GetProductsAsync(keyword, pageNumber, pageSize, "");
			var totalProducts = _productService.GetAllProducts().Count();
			var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

			ViewBag.CurrentPage = pageNumber;
			ViewBag.TotalPages = totalPages;

			return View(products);
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
		//[Route("DIAMONDJEWELERY")]



	}
}
