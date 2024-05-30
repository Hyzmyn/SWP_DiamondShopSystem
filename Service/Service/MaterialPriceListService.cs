using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class MaterialPriceListService
    {
        private MaterialPriceListRepository _repo = new MaterialPriceListRepository();

        public List<MaterialPriceList> GetMaterialPriceList() => _repo.GetAll();

        public MaterialPriceList? GetMaterialPriceList(int id) => _repo.Get(id);

        public List<MaterialPriceList> SearchMaterialPriceList(string keyword)
            => _repo.GetAll().Where(x => x.Id.ToString().Contains(keyword)).ToList();

        public void AddMaterialPriceList(MaterialPriceList materialPriceList) => _repo.Create(materialPriceList);

        public void UpdateMaterialPriceList(MaterialPriceList materialPriceList) => _repo.Update(materialPriceList);

        public void DeleteMaterialPriceList(int id) => _repo.Delete(id);

    }
}
