
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WarrantyRepository : BaseRepository<Warranty>, IWarrantyRepository
    {
        private DiamondShopContext _db;
        public WarrantyRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
        public async Task AddWarrantyAsync(int userId)
        {
            var order = await _db.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .ThenInclude(o => o.Gems)
                .FirstOrDefaultAsync(o => o.UserID == userId && !o.OrderStatus);

            if (order == null) return;

            var warranties = new List<Warranty>();

            foreach (var orderDetail in order.OrderDetails)
            {
                string instance = $"{orderDetail.Product.ProductCode} {orderDetail.Product.Gems.GemCode}";
                decimal pricePerUnit = orderDetail.Price / orderDetail.Quantity;

                switch (pricePerUnit)
                {
                    case <= 100:
                        instance += " BASIC-04";
                        break;
                    case <= 200:
                        instance += " ELITE-03";
                        break;
                    case <= 300:
                        instance += " TECH-02";
                        break;
                    default:
                        instance += " ACME-01";
                        break;
                }

                for (int i = 0; i < orderDetail.Quantity; i++)
                {
                    var warranty = new Warranty
                    {
                        WarrantyStatus = true,
                        OrderID = order.OrderID,
                        ProductID = orderDetail.ProductID,
                        BuyDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(36),
                        Instance = instance
                    };

                    warranties.Add(warranty);
                }
            }

            await _db.Warranties.AddRangeAsync(warranties);
            order.OrderStatus = true;
            await _db.SaveChangesAsync();
        }
    }
}
