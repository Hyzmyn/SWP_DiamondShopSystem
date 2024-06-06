using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDiscountService
    {
        public List<Discount> GetAllDiscount();

        public Discount? GetDiscount(int id);

        public List<Discount> SearchDiscount(string keyword);

        public void AddDiscount(Discount discount);
        public void UpdateDiscount(Discount discount);

        public void DeleteDiscount(int id);
    }
}
