using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class DiscountRepository
    {
        private DiamondShopContext _db;
        public List<Discount> GetAll()
        {
            _db = new();
            return _db.Discounts.ToList();

        }
        public Discount? Get(int id)
        {
            _db = new();
            return _db.Discounts.FirstOrDefault();
        }
        public Discount? GetByID(int id)
        {
            _db = new();
            return _db.Discounts.FirstOrDefault();
        }
        public void Create(Discount discount)
        {
            _db = new();
            _db.Discounts.Add(discount);
            _db.SaveChanges();
        }
        public void Update(Discount discount)
        {
            _db = new();
            _db.Discounts.Update(discount);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db = new();
            var Discount = _db.Discounts.FirstOrDefault(x => x.DiscountId == id);
            if (Discount != null )
            {
                _db.Discounts.Remove(Discount);
                _db.SaveChanges();
            }
        }


    }
}
