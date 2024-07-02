using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WalletPointService : IWalletService
    {
        private IWalletPointRepository _repo;
        private readonly DiamondShopContext _context;

        public WalletPointService(IWalletPointRepository repo, DiamondShopContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task AddWalletPointAsync(int userId, int point)
        {
            WalletPoint wallet = null;

            wallet = new WalletPoint
            {
                UserID = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Point = point
            };
            await _context.WalletPoints.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public Task DeleteWalletPointAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WalletPoint> GetWalletPointByIdAsync(int walletPoint)
        {
            throw new NotImplementedException();
        }

        public async Task<WalletPoint> GetWalletPointByUserIdAsync(int userId)
        {
            var wallet = await _context.WalletPoints.FirstOrDefaultAsync(o=>o.UserID==userId);
            return wallet;
        }

        public Task<List<WalletPoint>> GetWalletPointsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatWalletPointAsync(int id, decimal point)
        {
           await _repo.UpdateWalletPointAsync(id, point);
           
        }
    }
}
