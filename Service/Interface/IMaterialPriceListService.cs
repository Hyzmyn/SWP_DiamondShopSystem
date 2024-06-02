using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMaterialPriceListService
    {
        public List<MaterialPriceList> GetMaterialPriceList();

        public MaterialPriceList? GetMaterialPriceList(int id);

        public List<MaterialPriceList> SearchMaterialPriceList(string keyword);


        public void AddMaterialPriceList(MaterialPriceList materialPriceList);
        public void UpdateMaterialPriceList(MaterialPriceList materialPriceList);

        public void DeleteMaterialPriceList(int id);
    }
}
