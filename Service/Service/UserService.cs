using Repository.Entities;
using Repository.Interface;
using Repository.Repositories;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo = null;

        public UserService()
        {
            if(_repo == null)
            {
                _repo = new UserRepository();
            }
        }

        public List<User> GetAllUser() => _repo.GetAll();

        public List<User> SearchUser(string keyword)
            => _repo.GetAll().Where(x => x.UserId.ToString().Equals(keyword.ToLower()) ||
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
