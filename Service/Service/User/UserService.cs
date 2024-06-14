
using Repository.Interface;
using Repository.Models;
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


        public List<User> GetUsers(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            if (pageNumber <= 0 && pageSize <= 0)
            {
                pageSize = defaultPageSize;
                pageNumber = 1;
            }
            IQueryable<User> user = _repo.QueryStart();

            if (keyword != null && keyword.Length > 0)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    user = user.Where(x =>
                    x.UserID.ToString().Contains(keyword.ToLower()) ||
                    x.Username.ToLower().Contains(keyword.ToLower()));
                }
            }

            user = user.OrderBy(x => x.Username);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "username_desc": user = user.OrderByDescending(x => x.Username); break;
                }
            }

            List<User> usersList = new List<User>();
            usersList = user.OrderBy(x => x.UserID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalUsers = usersList.Count();
            var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalUsers / pageSize) : 0;

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            return usersList;
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
                throw new Exception($"User with id: {user.UserID} doesn't exists");
            }

        }


        public void AddUser(User user)
        {
            var existingUser = _repo.GetUsername(user.Username);
            if (existingUser == null)
            {
                int maxUserId = _repo.GetMaxUserId();
                user.UserID = maxUserId + 1;
                user.RoleID = 4;
                user.UserStatus = true;
                _repo.Create(user);
                _repo.Save();
            }
            else
            {
                throw new Exception($"Username: {user.Username} already exists");
            }

        }

        public void UpdateUser(User user)
        {
            var existingUser = _repo.Get(user.UserID);
            if (existingUser != null)
            {
                user.UserID = existingUser.UserID;
                _repo.Update(user);
                _repo.Save();
            }
            else
            {
                throw new Exception($"User with id: {user.UserID} doesn't exists");
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
