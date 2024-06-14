
using Repository.Interface;
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
        private IGemRepository _repo;


        public GemService(IGemRepository repo)
        {
            _repo = repo;
        }


        
    }
}
