using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Service.Interface;


namespace SWP.Areas.SaleStaff.Controllers
{
    [Area("salestaff")]
    [Route("salestaff")]
    [Route("salestaff/homesalestaff")]
    public class HomeSaleStaffController : Controller
    {
        private readonly IProductService _productService;
        DiamondShopContext db = new DiamondShopContext();
        [Route("")]
        [Route("salestaff")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("productlist")]
        public IActionResult ProductList()
        {
            var lstProDuct = db.Products.ToList();
            return View(lstProDuct);
        }

        [Route("createproduct")]
        [HttpGet] //đưa dữ liệu lên
        public IActionResult createproduct()
        {
            ViewBag.categoryid = new SelectList(db.ProductCategories.ToList(), "categoryid", "categoryname");
            return View();
        }

        [Route("createproduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("ProductList");
            }
            return View();
        }
    



        [Route("historyorder")]


        public IActionResult HistoryOrder()
        {
            return View();
        }


    }
}
