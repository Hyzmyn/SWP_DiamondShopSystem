using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class MaterialPriceList
    {
        [Key]
        public int ID { get; set; }
        public int MaterialID { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public DateTime EffDate { get; set; }

        [ForeignKey("MaterialID")]
        public virtual ProductMaterial ProductMaterial { get; set; }
    }
}
