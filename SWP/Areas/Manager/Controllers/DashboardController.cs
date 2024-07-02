using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.ViewModel;
using System.Diagnostics;

namespace SWP.Areas.Manager.Controllers
{
    [Area("manager")]
    public class DashboardController : Controller
    {
        
        private readonly DiamondShopContext _context;

        public DashboardController(DiamondShopContext context)
        {
            _context = context;
        }

        [HttpGet("/dashboard/")]
        public IActionResult Index()
        {
            StatisticBy(DateTime.Today, DateTime.Today);
            int currentYear = DateTime.Now.Year;
            var data = Chart(currentYear);
            ViewBag.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            return View();

        }
        [HttpPost("/statistic/")]
        public IActionResult Statictis(DateTime startDate, DateTime endDate)
        {
            int currentYear = DateTime.Now.Year;
            var data = Chart(currentYear);

            ViewBag.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
           
            StatisticBy(startDate, endDate);
           

            return View("Index");
        }
        [HttpGet("/datachart/")]
        public ActionResult GetDataChart(int year)
        {

            StatisticBy(DateTime.Today, DateTime.Today);
            var chart = Chart(year);
            return Json(chart);
        }




        private double CalculatePercentageChange(decimal oldValue, decimal newValue)
        {
            if (oldValue == newValue)
            {
                return 0;
            }
            else if (oldValue == 0)
            {
                
                return (double)newValue * 100;
            }

            return (double)((newValue - oldValue) / oldValue) * 100;
        }


        private int CalculatePercentageInt(int oldValue, int newValue)
        {


            if (oldValue == newValue)
            {
                return 0;
            }
            else if (oldValue == 0)
            {
                return 100; // Nếu giá trị cũ là 0, phần trăm tăng là 100%
            }

            return ((newValue - oldValue) / oldValue) * 100;
        }
        private void StatisticBy(DateTime startDate, DateTime endDate)
        {
            Debug.WriteLine($"startDate: {startDate}, endDate: {endDate}");
            if (startDate > endDate)
            {
                TempData["ErrorMessage"] = "The start date must be less than the end date!";
               
            } else
            {

                DateTime adjustedStartDate = startDate;


                DateTime adjustedEndDate = endDate.AddHours(23).AddMinutes(59);
                decimal currentPeriodTotal = _context.Orders
               .Where(o => o.TimeOrder >= adjustedStartDate && o.TimeOrder <= adjustedEndDate)
               .Sum(o => o.TotalPrice);

                DateTime previousStartDate = adjustedStartDate.AddYears(-1);
                DateTime previousEndDate = adjustedEndDate.AddYears(-1);

                decimal previousPeriodTotal = _context.Orders
                    .Where(o => o.TimeOrder >= previousStartDate && o.TimeOrder <= previousEndDate)
                    .Sum(o => o.TotalPrice);

                double percentageChange = CalculatePercentageChange(previousPeriodTotal, currentPeriodTotal);

                int ordersCount = _context.Orders
                    .Count(o => o.TimeOrder >= adjustedStartDate && o.TimeOrder <= adjustedEndDate);

                int totalUserCount = _context.Users
                    .Count(u => u.CreatedAt >= adjustedStartDate && u.CreatedAt <= adjustedEndDate);

                ViewBag.CurrentPeriodTotal = currentPeriodTotal;
                ViewBag.PercentageChange = percentageChange;
                ViewBag.OrdersCount = ordersCount.ToString("#,##0");
                ViewBag.TotalUserCount = totalUserCount.ToString("#,##0");
            }

            ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");




        }

        public Tuple<List<MonthlyOrderTotal>> Chart(int year)
        {
            var distinctYears = _context.Orders
                .Where(o => o.TimeOrder != null) 
                .Select(o => o.TimeOrder.Year) 
                .Distinct() 
                .OrderByDescending(year => year) 
                .ToList();

            ViewBag.AccountYear = distinctYears;

            ViewBag.AccountYear = distinctYears;
            var totalOrdersPerMonth = _context.Orders
                .Where(o => o.TimeOrder.Year == year)  
                .GroupBy(o => new { o.TimeOrder.Year, o.TimeOrder.Month })
                .Select(g => new MonthlyOrderTotal
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(o => o.TotalPrice),
                    TotalOrders = g.Count()
                })
                .OrderBy(result => result.Year)
                .ThenBy(result => result.Month)
                .ToList();
            ViewBag.Year = year;

            return Tuple.Create(totalOrdersPerMonth);
        }

    }
}
