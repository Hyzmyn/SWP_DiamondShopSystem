using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DiscountService
    {
        private DiscountRepository _repo = new DiscountRepository();

        public List<Discount> GetAllDiscount() => _repo.GetAll();

        public Discount? GetDiscount(int id) => _repo.Get(id);

        public List<Discount> SearchDiscount(string keyword)
            => _repo.GetAll().Where(x => x.DiscountId.ToString().Contains(keyword)).ToList();

        public void AddDiscount(Discount discount) => _repo.Create(discount);

        public void UpdateDiscount(Discount discount) => _repo.Update(discount);

        public void DeleteDiscount(int id) => _repo.Delete(id);

    }
}
