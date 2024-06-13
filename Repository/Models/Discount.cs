using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; }
        public int OrderID { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime DiscountDate { get; set; }
        public bool DiscountStatus { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
