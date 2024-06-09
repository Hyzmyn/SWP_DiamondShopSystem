using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FeedBackRepository : BaseRepository<FeedBack>, IFeedBackRepository
    {
        private DiamondShopContext _db;
        public FeedBackRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
    }
}
