
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Users 
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync(string keyword, int pageNumber, int pageSize, string sortBy);
        Task DeleteUserAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User> LoginAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);

    }
}
