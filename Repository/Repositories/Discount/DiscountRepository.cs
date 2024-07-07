
using Microsoft.EntityFrameworkCore;
using Repository.Models;




namespace Repository.Repositories
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        private DiamondShopContext _db;
        public DiscountRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
        public async Task<Discount> GetDiscountByCodeAsync(string discountCode)
        {
            return await _db.Discounts
                .Where(d => d.DiscountCode == discountCode && d.DiscountStatus)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Discount>> GetDiscountsByUserPointAsync(int userId)
        {
            decimal userPoint = await GetUserPointAsync(userId);

            decimal thresholdPoint = userPoint / 10;

            var discounts = await _db.Discounts
                .Where(d => d.DiscountAmount < thresholdPoint)
                .ToListAsync();

            return discounts;
        }

        private async Task<decimal> GetUserPointAsync(int userId)
        {
            var userPoint = await _db.WalletPoints
                .Where(wp => wp.UserID == userId)
                .Select(wp => wp.Point)
                .FirstOrDefaultAsync();

            return userPoint;
        }
    }
}
