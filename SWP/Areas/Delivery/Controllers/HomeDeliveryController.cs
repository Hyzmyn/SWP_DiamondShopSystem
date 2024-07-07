using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace SWP.Areas.Delivery.Controllers
{
    [Area("delivery")]
    [Route("delivery")]
    [Route("delivery/homedelivery")]
    public class HomeDeliveryController : Controller
    {
        private readonly DiamondShopContext context;

        public HomeDeliveryController(DiamondShopContext diamondShopContext ) 
        {
            context = diamondShopContext;
        }


        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var orders = context.Orders.Include(o => o.User).ToList();
            return View(orders);
        }


        [Route("Edit")]
        public IActionResult Edit(int orderId)
        {
            var order = context.Orders.FirstOrDefault(p => p.OrderID == orderId);
            if (order == null)
            {
                return RedirectToAction("index", "delivery");
            }
            var orderNew = new Order
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                TotalPrice = order.TotalPrice,
                TimeOrder = order.TimeOrder,
                Note = order.Note,
                OrderStatus = order.OrderStatus
            };

            ViewData["OrderID"] = order.OrderID;

            return View(orderNew);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public IActionResult Edit( Order order)
        {
            var oldOrder = context.Orders.FirstOrDefault(o => o.OrderID == order.OrderID);
            if (oldOrder == null)
            {
                return RedirectToAction("index", "delivery");
            }

            if (!ModelState.IsValid)
            {
                ViewData["OrderID"] = order.OrderID;
                return View(order);
            }

            oldOrder.OrderStatus = order.OrderStatus;

            context.Update(oldOrder);
            context.SaveChanges();

            TempData["Message"] = "Order updated successfully.";
            return RedirectToAction("index", "delivery");
        }

    }
}
