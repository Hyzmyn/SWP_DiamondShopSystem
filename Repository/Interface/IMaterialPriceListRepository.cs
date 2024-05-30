using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IMaterialPriceListRepository
    {
        public List<MaterialPriceList> GetAll();


        public MaterialPriceList? Get(int id);


        public MaterialPriceList? GetByID(int id);


        public void Create(MaterialPriceList materialPriceList);


        public void Update(MaterialPriceList materialPriceList);


        public void Delete(int id);
        
    }
}
