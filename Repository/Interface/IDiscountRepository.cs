using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDiscountRepository
    {
        public List<Discount> GetAll();

        public Discount? Get(int id);

        public Discount? GetByID(int id);

        public void Create(Discount discount);

        public void Update(Discount discount);

        public void Delete(int id);
        
    }
}
