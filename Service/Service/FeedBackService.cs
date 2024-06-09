using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class FeedBackService : IFeedBackService
    {
        private IFeedBackRepository _repo;
        public FeedBackService(IFeedBackRepository repo)
        {
            _repo = repo;
        }
    }
}
