
using Repository.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IWarrantyRepository : IBaseRepository<Warranty>
    {
        Task AddWarrantyAsync(int userId);
        Task<Warranty> GetWarrantyByProductAndOrderAsync(int productId, int orderId);
    }
}
