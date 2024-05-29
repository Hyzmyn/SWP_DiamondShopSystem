using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        User? Get(string username);
        List<User> GetAll();
        User? GetById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(int id);

    }
}
