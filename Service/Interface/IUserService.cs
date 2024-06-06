using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        List<User> GetAllUser();
        List<User> SearchUser(string keyword);
        User? GetUser(int id);
        void DeleteUser(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        User? Login(string username, string password);
    }
}
