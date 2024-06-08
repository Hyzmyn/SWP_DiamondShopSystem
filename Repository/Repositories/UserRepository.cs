using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private DiamondShopContext _db;

        public UserRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
        //public UserRepository()
        //{
        //    _db = new();
        //}

        public User? GetUsername(string username)
        {
            return _db.Users.FirstOrDefault(x => x.UserName == username);
        }
        //public List<User> GetAll()
        //{
        //    return _db.Users.ToList();
        //}

        public int GetMaxUserId()
        {
            // Get the maximum UserID from the database
            int maxUserId = _db.Users.Max(u => u.UserId);
            return maxUserId;
        }

        //public User? GetById(int id)
        //{
        //    return _db.Users.Find(id);
        //}

        //public void Create(User user)
        //{
        //    User u = GetById(user.UserId);
        //    if (u != null)
        //    {
        //        _db.Users.Add(user);
        //        _db.SaveChanges();
        //    }

        //}

        //public void Update(User user)
        //{
        //    User u = GetById(user.UserId);
        //    if (u != null)
        //    {
        //        u.UserId = user.UserId;
        //        u.UserName = user.UserName;
        //        u.Password = user.Password;
        //        u.Email = user.Email;
        //        u.PhoneNumber = user.PhoneNumber;
        //        u.Address = user.Address;
        //        u.RoleId = user.RoleId;
        //        u.UserStatus = user.UserStatus;
        //        u.NiSize = user.NiSize;
        //        _db.Users.Update(u);
        //        _db.SaveChanges();
        //    }
        //}
        //public void Delete(int id)
        //{
        //    User user = GetById(id);

        //    if (user != null)
        //    {
        //        _db.Users.Remove(user);
        //        _db.SaveChanges();
        //    }
        //}
    }
}
