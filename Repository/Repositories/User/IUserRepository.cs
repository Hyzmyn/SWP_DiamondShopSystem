using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetLoginAsync(string username, string password);

        Task<User> GetUserByIdAsync(int userId);

    }
}
