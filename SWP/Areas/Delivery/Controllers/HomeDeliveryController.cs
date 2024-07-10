using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Enum;
using System.Threading.Tasks;

namespace SWP.Areas.Delivery.Controllers
{
    [Area("delivery")]
    [Route("delivery")]
    [Route("delivery/homedelivery")]
    public class HomeDeliveryController : Controller
    {
        private readonly DiamondShopContext context;

        public HomeDeliveryController(DiamondShopContext diamondShopContext)
        {
            context = diamondShopContext;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            
            var orders = context.Orders
                                .Include(o => o.User)
                                .Where(o => o.OrderStatus == true)
                                .ToList();
            return View(orders);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("UpdateDeliveryStatus")]
        public async Task<IActionResult> UpdateDeliveryStatus(int orderID, bool deliveryStatus)
        {
            var order = await context.Orders.FindAsync(orderID);
            if (order == null)
            {
                return Json(new { success = false });
            }

            order.DeliveryStatus = deliveryStatus;
            context.Update(order);
            await context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
