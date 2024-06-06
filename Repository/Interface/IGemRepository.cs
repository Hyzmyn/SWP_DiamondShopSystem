using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IGemRepository
    {
        public Gem? Get(string GemName);
        public List<Gem> GetAll();

        public Gem? GetById(int id);

        public void Create(Gem gem);

        public void Update(Gem gem);
        public void Delete(int id);
    }
}
