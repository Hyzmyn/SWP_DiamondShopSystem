using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DiamondShopContext _db;

        public UserRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }

        private DateTime GetVietnamTime()
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
        }

        public async Task<User?> GetLoginAsync(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserID == userId);
        }

        public void Update(User entity)
        {
            _db.Users.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task SavePasswordResetTokenAsync(string email, string token)
        {
            var user = await FindByEmailAsync(email);
            if (user != null)
            {
                user.ResetToken = token;
                user.ResetTokenExpires = GetVietnamTime().AddHours(1);
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByResetTokenAsync(string token)
        {
            var currentTime = GetVietnamTime();
            return await _db.Users.FirstOrDefaultAsync(x => x.ResetToken == token && x.ResetTokenExpires > currentTime);
        }

        public async Task<bool> ResetPasswordAsync(string token, string email, string newPassword)
        {
            var currentTime = GetVietnamTime();
            var user = await _db.Users.FirstOrDefaultAsync(x => x.ResetToken == token && x.Email == email && x.ResetTokenExpires > currentTime);
            if (user != null)
            {
                user.Password = newPassword;
                user.ResetToken = null;
                user.ResetTokenExpires = null;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
