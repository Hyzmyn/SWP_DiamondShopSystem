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
        public List<User> GetAllUser(string keyword, int pageNumber, int pageSize);
        public void DeleteUser(int id);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public User? Login(string username, string password);
    }
}
