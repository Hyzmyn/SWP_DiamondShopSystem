using Repository.Entities;
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
        private UserRepository _repo;



        public List<User> GetAllUser(string keyword, int pageNumber, int pageSize)
        {
            var user = _repo.Get().Where(u => u.UserStatus == true).ToList();

            if (keyword != null && keyword.Length > 0)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    user = user.Where(x =>
                    x.UserId.ToString().Contains(keyword.ToLower()) ||
                    x.UserName.ToLower().Contains(keyword.ToLower())).ToList();
                }
            }

            var totalUsers = user.Count();
            var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalUsers / pageSize) : 0;

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            if (pageNumber > 0 && pageSize > 0)
            {
                user = user.OrderBy(x => x.UserId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            return user;
        }

        public void DeleteUser(int id)
        {
            var user = _repo.Get(id);
            if (user != null)
            {
                _repo.Delete(user);
            }
            else
            {
                throw new Exception($"No user found with id: {id}");
            }
        }


        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new Exception("User cannot be null");
            }
            var existingUser = _repo.Get(user);
            if (existingUser != null)
            {
                throw new Exception($"User with id: {user.UserId} already exists");
            }
            int maxUserId = _repo.GetMaxUserId();
            user.UserId = maxUserId + 1;
            user.RoleId = 4;
            user.UserStatus = true;
            _repo.Create(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new Exception("User cannot be null");
            }
            var existingUser = _repo.Get(user);
            if (existingUser != null)
            {
                user.UserId = existingUser.UserId;
                _repo.Update(user);
            }
            else
            {
                throw new Exception("User doesn't exist");
            }
        }

        public User? Login(string username, string password)
        {
            User account = _repo.Get(username);

            if (account == null)
            {
                throw new Exception("Incorrect username or password");
            }
            return account != null && account.Password == password ? account : null;
        }

    }
}
