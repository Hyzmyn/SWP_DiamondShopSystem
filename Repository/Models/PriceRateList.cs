using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class PriceRateList
    {
        [Key]
        public int PriceRateID { get; set; }
        public Decimal PriceRate { get; set; }
        public DateTime EffDate { get; set; }


        public virtual Product Products { get; set; }
    }
}
