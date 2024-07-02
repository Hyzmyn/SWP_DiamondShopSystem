using Repository.Models;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IWalletPointRepository : IBaseRepository<WalletPoint>
    {
        Task<WalletPoint> GetWalletPointByUserIdAsync(int userId);
        Task UpdateWalletPointAsync(int id, decimal point);
    }
}


