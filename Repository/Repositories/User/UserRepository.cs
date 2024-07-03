using Microsoft.EntityFrameworkCore;
using Repository.Models;
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

        public async Task<User?> GetLoginAsync(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserID == userId);
        }
    }
}
