using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;


namespace SWP.Areas.SaleStaff.Controllers
{
    [Area("salestaff")]
    [Route("salestaff")]
    [Route("salestaff/homesalestaff")]
    public class HomeSaleStaffController : Controller
    {
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
        [HttpGet]//đưa dữ liệu lên
        public IActionResult CreateProduct()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories.ToList(), "CategoryID", "CategoryName");
            return View();
        }

        [Route("createproduct")]
        [HttpPost]//lưu về trong cơ sở dữ liệu
        [ValidateAntiForgeryToken]//kiểm tra xem dữ liệu có nhập chĩnh xác vali ko
        public IActionResult CreateProduct(Product sp)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(sp);
                db.SaveChanges();
                return RedirectToAction("ProductList");
            }
            return View(sp);
        }



        [Route("historyorder")]


        public IActionResult HistoryOrder()
        {
            return View();
        }


    }
}
