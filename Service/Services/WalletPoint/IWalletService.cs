using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IWalletService
    {
        Task<List<WalletPoint>> GetWalletPointsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteWalletPointAsync(int id);
        Task AddWalletPointAsync(int userId, int point);
        Task UpdatWalletPointAsync(int id, decimal point);
        Task<WalletPoint> GetWalletPointByUserIdAsync(int userId);
        Task<WalletPoint> GetWalletPointByIdAsync(int walletPoint);
    }
}
