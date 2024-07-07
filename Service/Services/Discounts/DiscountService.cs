using Repository.Repositories;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DiscountService : IDiscountService
    {
        private IDiscountRepository _repo;
        public DiscountService(IDiscountRepository repo)
        {
            _repo = repo;
        }

        public Task AddDiscountAsync(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDiscountAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Discount>> GetDiscountsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDiscountAsync(Discount discount)
        {
            throw new NotImplementedException();
        }
        public async Task<Discount> GetDiscountAsync(string discountCode)
        {
            return await _repo.GetDiscountByCodeAsync(discountCode);
        }
        public async Task<List<Discount>> GetDiscountsByUserPointAsync(int userId)
        {
            return await _repo.GetDiscountsByUserPointAsync(userId);
        }

    }
}
