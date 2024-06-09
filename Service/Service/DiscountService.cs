using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DiscountService : IDiscountService
    {
        private IDiscountRepository _repo;
        public DiscountService(IDiscountRepository repo)
        {
            _repo = repo;
        }
    }
}
