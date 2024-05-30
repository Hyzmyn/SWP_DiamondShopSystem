using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class FeedBackService
    {
        private FeedBackRepository _repo = new FeedBackRepository();

        public List<FeedBack> GetAllFeedBack() => _repo.GetAll();

        public FeedBack? GetFeedBack(int id) => _repo.GetByID(id);
        
        public void AddFeedBack(FeedBack feedback) => _repo.Create(feedback);

        public void UpdateFeedBack(FeedBack feedback) => _repo.Update(feedback);

        public void DeleteFeedBack(int id) => _repo.Delete(id);
    }
}
