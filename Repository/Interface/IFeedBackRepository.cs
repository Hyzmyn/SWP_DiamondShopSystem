using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IFeedBackRepository
    {
        public List<FeedBack> GetAll();


        public FeedBack? GetByID(int id);


        public void Create(FeedBack feedback);


        public void Update(FeedBack feedback);


        public void Delete(int id);
        
    }
}
