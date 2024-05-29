using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MaterialPriceListRepository
    {
        private DiamondShopContext _db;
        public List<MaterialPriceList> GetAll()
        {
            _db = new();
            return _db.MaterialPriceLists.ToList();
        }
        public MaterialPriceList? Get(int id)
        {
            _db = new();
            return _db.MaterialPriceLists.FirstOrDefault();
        }
        public MaterialPriceList? GetByID(int id)
        {
            _db = new();
            return _db.MaterialPriceLists.FirstOrDefault();
        }
        public void Create(MaterialPriceList materialPriceList)
        {
            _db = new();
            _db.MaterialPriceLists.Add(materialPriceList);
            _db.SaveChanges();
        }
        public void Update(MaterialPriceList materialPriceList)
        {
            _db = new();
            _db.MaterialPriceLists.Update(materialPriceList);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db = new();
            var MaterialPriceList = _db.MaterialPriceLists.FirstOrDefault(x => x.MaterialId == id);
            if (MaterialPriceList != null)
            {
                _db.MaterialPriceLists.Remove(MaterialPriceList);
                _db.SaveChanges() ;
            }
        }
    }
}
