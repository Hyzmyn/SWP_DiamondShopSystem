
using Repository.Interface;
using Repository.Repositories;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _repo;
        public ProductCategoryService(IProductCategoryRepository repo)
        {
            _repo = repo;
        }
    }
}
