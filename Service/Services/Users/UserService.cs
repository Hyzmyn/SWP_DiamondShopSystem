
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repo;


        public UserService(IUserRepository repo)
        {
            _repo = repo;
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
            var existingUser = await _repo.GetUsernameAsync(user.Username);
            if (existingUser == null)
            {
                int maxUserId = await _repo.GetMaxIdAsync();
                user.UserID = maxUserId + 1;
                user.RoleID = 5;
                user.UserStatus = true;
                await _repo.CreateAsync(user);
                await _repo.SaveAsync();
            }
            else
            {
                throw new Exception($"Username: {user.Username} already exists");
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
            User account = await _repo.GetUsernameAsync(username);

            if (account == null)
            {
                throw new Exception($"Username does not exist");
            }
            else if (account.Password != password)
            {
                throw new Exception($"Password is incorrect");
            }
            return account;
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _repo.GetUserByIdAsync(userId);
        }
    }
}
