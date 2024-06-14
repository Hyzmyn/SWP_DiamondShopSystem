
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        public List<User> GetUsers(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        public void DeleteUser(int id);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public User? Login(string username, string password);
    }
}
