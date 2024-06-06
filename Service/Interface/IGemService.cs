using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IGemService
    {
        public List<Gem> GetAllGem();
        public Gem? GetAnGem(int id);
        public List<Gem> SerchGem(int id);
        public void AddGem(Gem gem);
        public void UpdateGem(Gem gem);
        public void DeleteGem(int id);
    }
}
