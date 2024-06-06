using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class GemService
    {
        private GemRepository _repo = new();
        public List<Gem> GetAllGem() => _repo.GetAll();
        public Gem? GetAnGem(int id) => _repo.GetById(id);
        public List<Gem> SerchGem(int id) => _repo.GetAll().Where(x => x.GemId.Equals(id)).ToList();
        public void AddGem(Gem gem) => _repo.Create(gem);
        public void UpdateGem(Gem gem) => _repo.Update(gem);
        public void DeleteGem(int id) => _repo.Delete(id);
    }
}
