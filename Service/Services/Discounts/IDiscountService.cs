
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IDiscountService
    {
        Task<List<Discount>> GetDiscountsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteDiscountAsync(int id);
        Task AddDiscountAsync(Discount discount);
        Task UpdateDiscountAsync(Discount discount);
        Task<Discount> GetDiscountAsync(string discountCode);
    }
}
