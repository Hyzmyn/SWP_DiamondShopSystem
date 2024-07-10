
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services 
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync(string keyword, int pageNumber, int pageSize, string sortBy);
        Task DeleteUserAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User> LoginAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);
        
        Task<bool> VerifyPasswordAsync(int userId, string password);
        Task<bool> ChangePasswordAsync(int userId, string newPassword);
        Task<User?> FindByEmailAsync(string email);
        Task SavePasswordResetTokenAsync(string email, string token);
        Task<User?> GetUserByResetTokenAsync(string token);
        Task<bool> ResetPasswordAsync(string token, string email, string newPassword);

    }
}
