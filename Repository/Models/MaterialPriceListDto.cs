using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class MaterialPriceListDto
    {
        public int MaterialID { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public DateTime EffDate { get; set; }
    }
}
