using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository
    {
        private DiamondShopContext _db;

        public List<User> GetAll()
        {
            _db = new ();
            return _db.Users.ToList();
        }
    }
}
