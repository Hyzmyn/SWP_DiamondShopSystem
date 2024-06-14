using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private DiamondShopContext _db;

        public UserRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }

        public User? GetUsername(string username)
        {
            return _db.Users.FirstOrDefault(x => x.Username == username);
        }

        public int GetMaxUserId()
        {
            // Get the maximum UserID from the database
            int maxUserId = _db.Users.Max(u => u.UserID);
            return maxUserId;
        }
    }
}
