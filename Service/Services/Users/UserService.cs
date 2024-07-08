
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repo;
        private readonly IPasswordHasher<User> _passwordHasher;


        public UserService(IUserRepository repo, IPasswordHasher<User> passwordHasher)
        {
            _repo = repo;
            _passwordHasher = _passwordHasher;
        }


        public async Task<List<User>> GetUsersAsync(string keyword, int pageNumber, int pageSize, string sortBy)
        {
			int defaultPageSize = 10;
			if (pageNumber <= 0 && pageSize <= 0)
            {
                pageSize = defaultPageSize;
                pageNumber = 1;
            }

            IQueryable<User> user = _repo.QueryStart();

            if (!string.IsNullOrEmpty(keyword))
            {
                user = user.Where(x =>
                    x.UserID.ToString().Contains(keyword.ToLower()) ||
                    x.Username.ToLower().Contains(keyword.ToLower()));
            }

            user = user.OrderBy(x => x.Username);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "username_desc":
                        user = user.OrderByDescending(x => x.Username);
                        break;
                }
            }

            var usersList = await user.OrderBy(x => x.UserID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalUsers = usersList.Count;
            var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalUsers / pageSize) : 0;

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            return usersList;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _repo.GetAsync(id);
            if (user != null)
            {
                _repo.Delete(user);
                await _repo.SaveAsync();
            }
            else
            {
                throw new Exception($"User with id: {id} doesn't exist");
            }
        }


        public async Task AddUserAsync(User user)
        {
            var existingUser = await _repo.GetLoginAsync(user.Username, user.Password);
            if (existingUser == null)
            {
                user.UserStatus = true;
                user.CreatedAt = DateTime.Now;                                                     
                await _repo.CreateAsync(user);
                await _repo.SaveAsync();
            }
            else
            {
                user = null;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _repo.GetAsync(user.UserID);
            if (existingUser != null)
            {
                user.UserID = existingUser.UserID;
                _repo.Update(user);
                await _repo.SaveAsync();
            }
            else
            {
                throw new Exception($"User with id: {user.UserID} doesn't exist");
            }
        }
        
        public async Task<User> LoginAsync(string username, string password)
        {
            User account = await _repo.GetLoginAsync(username, password);

            return account;
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _repo.GetUserByIdAsync(userId);
        }


        public async Task<bool> VerifyPasswordAsync(int userId, string currentPassword)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            if (user.Password == currentPassword)
            {
                return true;
            }
            else { return false; }
            
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newPassword)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.Password = newPassword;
            _repo.Update(user);
            await _repo.SaveAsync();
            return true;
        }
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _repo.FindByEmailAsync(email);
        }

        public async Task SavePasswordResetTokenAsync(string email, string token)
        {
            await _repo.SavePasswordResetTokenAsync(email, token);
        }

        public async Task<User?> GetUserByResetTokenAsync(string token)
        {
            return await _repo.GetUserByResetTokenAsync(token);
        }

        public async Task<bool> ResetPasswordAsync(string token, string email, string newPassword)
        {
            return await _repo.ResetPasswordAsync(token, email, newPassword);
        }


    }
}
