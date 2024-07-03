using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Repository.Models;
using Service;

using Service.Service.ViewModels;
using Service.Services;
using System.Diagnostics;
using System.Text.Json;


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
        public async Task<IActionResult> QuickView(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Product found: {JsonSerializer.Serialize(product)}");
            return Json(new
            {
                productID = product.ProductID,
                productName = product.ProductName,
                totalCost = product.TotalCost,
                imageUrl1 = product.ImageUrl1,
                imageUrl2 = product.ImageUrl2,
            });
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
