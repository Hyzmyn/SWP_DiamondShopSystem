using Microsoft.AspNetCore.Mvc;


namespace SWP.Areas.SaleStaff.Controllers
{
    [Area("salestaff")]
    [Route("salestaff")]
    [Route("salestaff/homesalestaff")]
    public class HomeSaleStaffController : Controller
    {
       
        [Route("")]
        [Route("salestaff")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("productlist")]
        public IActionResult ProductList()
        {
            return View();
        }


        [Route("historyorder")]


        public IActionResult HistoryOrder()
        {
            return View();
        }


    }
}
