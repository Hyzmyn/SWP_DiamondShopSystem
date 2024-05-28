using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        private UserRepository _repo = new UserRepository();

        public List<User> GetAllUser() => _repo.GetAll();

        public List<User> SearchUser(string keyword)
            => _repo.GetAll().Where(x => x.UserId.ToString().Contains(keyword.ToLower()) ||
                                         x.UserName.ToLower().Contains(keyword.ToLower())).ToList();

        public User? GetUser(int id) => _repo.GetById(id);

        public void DeleteUser(int id) => _repo.Delete(id);

        public void AddUser(User user) => _repo.Create(user);

        public void UpdateUser(User user) => _repo.Update(user);

        public User? Login(string username, string password)
        {
            User account = _repo.Get(username);
            return account != null && account.Password == password ? account : null;
        }

    }
}
