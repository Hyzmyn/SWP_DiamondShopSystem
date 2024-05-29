using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository
    {
        private DiamondShopContext _db;

        public User? Get(string username)
        {
            _db = new();
            return _db.Users.FirstOrDefault(x => x.UserName == username);
        }
        public List<User> GetAll()
        {
            _db = new();
            return _db.Users.ToList();
        }

        public User? GetById(int id)
        {
            _db = new();
            return _db.Users.Find(id);
        }

        public void Create(User user)
        {
            _db = new();
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            _db = new();
            _db.Users.Update(user);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db = new();
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }
    }
}
