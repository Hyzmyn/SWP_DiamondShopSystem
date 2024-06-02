using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFeedBackService
    {
        public List<FeedBack> GetAllFeedBack();

        public FeedBack? GetFeedBack(int id);

        public void AddFeedBack(FeedBack feedback);

        public void UpdateFeedBack(FeedBack feedback);
        public void DeleteFeedBack(int id);
    }
}
