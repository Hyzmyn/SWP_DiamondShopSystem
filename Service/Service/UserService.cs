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
        private IUserRepository _repo;


        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }


        public List<User> GetUsers(string keyword, int pageNumber, int pageSize)
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
                _repo.Save();
            }
            else
            {
                throw new Exception($"User with id: {user.UserId} doesn't exists");
            }

        }


        public void AddUser(User user)
        {
            var existingUser = _repo.GetUsername(user.UserName);
            if (existingUser == null)
            {
                int maxUserId = _repo.GetMaxUserId();
                user.UserId = maxUserId + 1;
                user.RoleId = 4;
                user.UserStatus = true;
                _repo.Create(user);
                _repo.Save();
            }
            else
            {
                throw new Exception($"Username: {user.UserName} already exists");
            }

        }

        public void UpdateUser(User user)
        {
            var existingUser = _repo.Get(user.UserId);
            if (existingUser != null)
            {
                user.UserId = existingUser.UserId;
                _repo.Update(user);
                _repo.Save();
            }
            else
            {
                throw new Exception($"User with id: {user.UserId} doesn't exists");
            }

        }

        public User? Login(string username, string password)
        {
            User account = _repo.GetUsername(username);

            if (account == null || account.Password != password)
            {
                throw new Exception($"Username or Password was incorrect");
            }
            return account;
        }
    }
}
