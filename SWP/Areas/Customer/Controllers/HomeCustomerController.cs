using Microsoft.AspNetCore.Mvc;

namespace SWP.Areas.Customer.Controllers
{
    [Area("customer")]
    [Route("customer")]
    [Route("customer/homecustomer")]
    public class HomeCustomerController : Controller
    {
        [Route("")]
        [Route("customer")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("customerinformation")]
        public IActionResult CustomerInformation()
        {
            return View();
        }

        [Route("historyofcustomer")]
        public IActionResult HistoryOfCustomer()
        {
            return View();
        }

        [Route("orderofcustomer")]
        public IActionResult OrderOfCustomer()
        {
            return View();
        }

        [Route("changepassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
