using Microsoft.AspNetCore.Mvc;
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


        [Route("historyorder")]


        public IActionResult HistoryOrder()
        {
            return View();
        }


    }
}
