using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
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
            return _db.Users.FirstOrDefault(x => x.UserName == username);
        }

        public int GetMaxUserId()
        {
            // Get the maximum UserID from the database
            int maxUserId = _db.Users.Max(u => u.UserId);
            return maxUserId;
        }
    }
}
