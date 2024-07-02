using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WalletPointRepository : BaseRepository<WalletPoint>, IWalletPointRepository
    {
        private DiamondShopContext _db;
        public WalletPointRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }

        public async Task<WalletPoint> GetWalletPointByUserIdAsync(int userId)
        {
            var wallet = await _db.WalletPoints
                            .FirstOrDefaultAsync(o => o.UserID == userId);
            return wallet;


        }

        public async Task UpdateWalletPointAsync(int id, decimal point)
        {
            var walletPoint = await _db.WalletPoints.FirstOrDefaultAsync(o => o.UserID == id);
            if (walletPoint != null)
            {
                walletPoint.Point += point;
                walletPoint.UpdatedAt = DateTime.Now;
                await _db.SaveChangesAsync();
            }
           
        }
    }
}
