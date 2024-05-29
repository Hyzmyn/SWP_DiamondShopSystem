using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FeedBackRepository
    {
        private DiamondShopContext _db;
        public List<FeedBack> GetAll() 
        {
            _db = new();
            return _db.FeedBacks.ToList();
        } 
        public FeedBack? GetByID(int id)
        {
            _db = new();
            return _db.FeedBacks.FirstOrDefault();
        }
        public void Create(FeedBack feedback)
        {
            _db = new();
            _db.FeedBacks.Add(feedback);
            _db.SaveChanges();
        }
        public void Update(FeedBack feedback)
        {
            _db = new();
            _db.FeedBacks.Update(feedback);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db = new();
            var FeedBack = _db.FeedBacks.FirstOrDefault(x => x.FeedbackId == id);
            if (FeedBack != null)
            {
                _db.FeedBacks.Remove(FeedBack);
                _db.SaveChanges();
            }
        }
    }
}
