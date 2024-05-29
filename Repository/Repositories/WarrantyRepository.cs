using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WarrantyRepository
    {
        private DiamondShopContext _db;


        public List<Warranty> GetAll()
        {
            _db = new();
            return _db.Warranties.ToList();
        }
        public Warranty? GetByID(int WarrantyID)
        {
            _db = new();
            return _db.Warranties.Find(WarrantyID);
        }
        public void Create(Warranty warranty)
        {
            _db = new();
            _db.Warranties.Add(warranty);
            _db.SaveChanges();
        }
        public void Update(Warranty warranty)
        {
            _db = new();
            _db.Warranties.Update(warranty);
            _db.SaveChanges();
        }
        public void Delete(int WarrantyID)
        {
            _db = new();
            var Warranty = _db.Warranties.FirstOrDefault(x => x.Equals(WarrantyID));
            if (Warranty != null)
            {
                _db.Warranties.Remove(Warranty);
                _db.SaveChanges();
            }

        }
    }
}
