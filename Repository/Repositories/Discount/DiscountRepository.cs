
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using Repository.Repositories.Base;



namespace Repository
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
            return await _db.Discounts.FirstOrDefaultAsync(d => d.DiscountCode == discountCode);
        }
    }
}
