using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GemRepository
    {
        private DiamondShopContext _db = new();
        public Gem? Get(string GemName)
        {
            _db = new();
            return _db.Gems.FirstOrDefault(x => x.GemName == GemName);
        }
        public List<Gem> GetAll()
        {
            _db = new();
            return _db.Gems.ToList();
        }

        public Gem? GetById(int id)
        {
            _db = new();
            return _db.Gems.Find(id);
        }

        public void Create(Gem gem)
        {
            _db = new();
            _db.Gems.Add(gem);
            _db.SaveChanges();
        }

        public void Update(Gem gem)
        {
            _db = new();
            _db.Gems.Update(gem);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db = new();
            var gem = _db.Gems.FirstOrDefault(x => x.GemId == id);

            if (gem != null)
            {
                _db.Gems.Remove(gem);
                _db.SaveChanges();
            }
        }

    }
}
