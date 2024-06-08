using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User? GetUsername(string username);
        public int GetMaxUserId();
        //List<User> GetAll();
        //User? GetById(int id);
        //void Create(User user);
        //void Update(User user);
        //void Delete(int id);

    }
}
