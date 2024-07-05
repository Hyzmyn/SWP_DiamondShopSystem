
using Repository.Models;

namespace Repository.Repositories
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {

        Task<Discount> GetDiscountByCodeAsync(string discountCode);
    }
}
